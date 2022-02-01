using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

[assembly: ZSharp.Core.LanguageEngine(typeof(ZSharp.Engine.Context))]

namespace ZSharp.Engine
{
    public class Context : Core.ILanguageEngine
    {
        private Dictionary<Core.DocumentInfo, SearchScope> _documentScopes = new();

        private interface IDocumentObject : IGenericCompilable<IDocumentObject> { }

        public static Context? CurrentContext { get; private set; }

        public TypeSystem? TypeSystem { get; private set; }

        public readonly SRFModule Module;

        private static readonly string _exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        private int _currentPass = 0;

        private readonly Evaluator _evaluator;

        public readonly ProjectScope Scope = new();

        private readonly List<BaseProcessor> _processors = new();

        public BaseProcessor? CurrentProcessor { get; private set; }

        public Context()
        {
            if (CurrentContext is not null) throw new InvalidOperationException();
            CurrentContext = this;
            var assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("SRF"), AssemblyBuilderAccess.RunAndCollect);
            Module = new(
                assembly.DefineDynamicModule("SRF"), 
                ModuleDefinition.CreateModule("SRF", ModuleKind.Console)
                );

            _evaluator = new(this);
        }

        public void Setup()
        {
            TypeSystem = new(this);

            _processors.AddRange(new BaseProcessor[]
            {
                new DocumentProcessor(this),
                new ObjectDesciptorProcessor(this),
                new GenericProcessor<IBuildable>(this),
                new DependencyFinder(this),
                //new GenericProcessor<ISRFResolvable>(this),
                //new GenericProcessor<ISRFCompilable>(this),
                new GenericProcessor<IResolvable>(this),
                new GenericProcessor<ICompilable>(this),
            });
        }

        public void AddAssemblyReference(string path)
        {
            AddAssemblyReference(Assembly.LoadFile(path));
        }

        public void AddAssemblyReference(Assembly assembly)
        {
            foreach (CustomOperatorAttribute customOperator in assembly.GetCustomAttributes<CustomOperatorAttribute>())
            {
                switch (customOperator.Fixity)
                {
                    case Core.OperatorFixity.Prefix:
                        Parser.Expression.AddPrefixOperator(customOperator.Name, customOperator.Precedence);
                        break;
                    case Core.OperatorFixity.Infix:
                        Parser.Expression.AddBinaryOperator(customOperator.Name, customOperator.Precedence, customOperator.Associativity);
                        break;
                    case Core.OperatorFixity.Postfix:
                        Parser.Expression.AddPostfixOperator(customOperator.Name, customOperator.Precedence);
                        break;
                    default:
                        break;
                }
            }

            foreach (CustomKeywordAttribute customKeyword in assembly.GetCustomAttributes<CustomKeywordAttribute>())
            {
                Parser.Expression.AddKeywordOperator(customKeyword.Name, customKeyword.Precedence, customKeyword.Fixity);
            }

            foreach (CustomKeywordLiteralAttribute literal in assembly.GetCustomAttributes<CustomKeywordLiteralAttribute>())
            {
                Parser.Expression.AddKeywordTerm(literal.Name);
            }

            foreach (CustomSurroundingOperatorAttribute surrounding in assembly.GetCustomAttributes<CustomSurroundingOperatorAttribute>())
            {
                Parser.Expression.AddSurroundingOperator(
                    surrounding.Left,
                    surrounding.Right,
                    surrounding.Precedence,
                    surrounding.IsPrefix,
                    surrounding.AllowMultiple
                    );
            }

            foreach (Module module in assembly.Modules)
            {
                foreach (System.Type type in module.GetTypes())
                {
                    if (!type.IsVisible) continue;

                    foreach (MethodInfo method in type.GetMethods())
                    {
                        if (method.GetCustomAttribute<KeywordOverloadAttribute>() is KeywordOverloadAttribute kw)
                        {
                            string name = kw.Keyword;
                            int numParameters = method.GetParameters().Length;
                            name = numParameters switch
                            {
                                0 => name,
                                1 => kw.IsPrefix ? name + '_' : '_' + name,
                                2 => '_' + name + '_',
                            };
                            Scope.GlobalScope.AddItem(new SRFFunctionOverload(new FunctionReference(method)), name);
                        }
                        else if (method.GetCustomAttribute<SurroundingOperatorOverloadAttribute>() is SurroundingOperatorOverloadAttribute sur)
                        {
                            string name = sur.Operator;
                            name = sur.IsPrefix ? name + '_' : '_' + name;
                            Scope.GlobalScope.AddItem(new SRFFunctionOverload(new FunctionReference(method)), name);
                        }
                        else if (method.GetCustomAttribute<OperatorOverloadAttribute>() is OperatorOverloadAttribute op)
                        {
                            string name = op.Operator;
                            name = method.GetParameters().Length == 1 ? op.IsPrefix ? name + '_' : '_' + name : '_' + name + '_';
                            Scope.GlobalScope.AddItem(new SRFFunctionOverload(new FunctionReference(method)), name);
                        }
                    }
                }
            }

            foreach (TypeInfo type in assembly.DefinedTypes)
            {
                if (!type.IsVisible || type.IsNested) continue;

                string name = type.Name;
                if (type.IsGenericTypeDefinition)
                    name = name[..name.IndexOf('`')];

                Scope.GetOrCreateNamespace(type.Namespace)
                    .AddItem(new GenericTypeOverload(new TypeReference(type)), name);

                foreach (MethodInfo method in type.GetMethods())
                {
                    if (!method.IsPublic) return;

                    Scope.AddItem(new SRFFunctionOverload(new MethodReference(method)));
                }
            }

            foreach (TypeInfo type in assembly.GetForwardedTypes())
            {
                if (!type.IsVisible || type.IsNested) continue;

                string name = type.Name;
                if (type.IsGenericTypeDefinition)
                    name = name[..name.IndexOf('`')];

                Scope.GetOrCreateNamespace(type.Namespace)
                    .AddItem(new GenericTypeOverload(new TypeReference(type)), name);
            }

            foreach (Module module in assembly.Modules)
            {
                foreach (MethodInfo method in module.GetMethods())
                {
                    if (!method.IsPublic) continue;

                    Scope.GlobalScope.AddItem(new FunctionOverload(
                        new FunctionReference(method)
                        ));
                }
            }
        }

        private void CopyDependenciesRecursive(AssemblyDefinition assembly, string dir)
        {
            foreach (AssemblyNameReference reference in assembly.MainModule.AssemblyReferences)
            {
                var dependency = Module.MC.AssemblyResolver.Resolve(reference);
                string referencePath = Path.GetFullPath(Path.GetFileName(dependency.MainModule.FileName), dir);
                if (!File.Exists(referencePath))
                    File.Copy(dependency.MainModule.FileName, referencePath);

                CopyDependenciesRecursive(dependency, dir);
            }
        }

        public void FinishCompilation(string path)
        {
            string dir = Path.GetDirectoryName(Path.GetFullPath(path))!;
            Module.MC.Assembly.Name.Name = Module.MC.Name = Path.GetFileNameWithoutExtension(path);
            
            Module.MC.Write(path);

            //CopyDependenciesRecursive(Module.MC.Assembly, dir);

            //using (StreamWriter configFile = File.CreateText(Module.MC.Name + ".runtimeconfig.json"))
            //{
            //    //Module.MC
            //    RuntimeConfig config = new();
            //    //config.runtimeOptions.TargetFramework = 
            //    Newtonsoft.Json.JsonSerializer.CreateDefault().Serialize(configFile, config);
            //}

            // we're gonna cheat a bit with this one. we need to generate App.runtimeconfig.json
            // to be able to run out application with 'dotnet app.runtimeconfig.json'
            string configFile = Path.GetFullPath(Module.MC.Name + ".runtimeconfig.json", dir);
            if (false)
            {
                var json = Newtonsoft.Json.JsonSerializer.Create(new()
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });
                using TextReader sourceConfig = File.OpenText(Path.GetFullPath("ZSharpCompiler.runtimeconfig.json", _exePath));
                RuntimeConfig cfg = (RuntimeConfig)json.Deserialize(sourceConfig, typeof(RuntimeConfig));

                if ((cfg.runtimeOptions.IncludeFrameworks?.Length ?? 0) > 0)
                {
                    cfg.runtimeOptions.Framework = cfg.runtimeOptions.IncludeFrameworks[0];
                    cfg.runtimeOptions.IncludeFrameworks = null;
                }

                using TextWriter targetConfig = File.CreateText(configFile);
                json.Serialize(targetConfig, cfg);
            }
        }

        public Core.IExpressionProcessor NextProcessor()
        {
            if (_currentPass >= _processors.Count) return null;
            return CurrentProcessor = _processors[_currentPass++];
        }

        public Core.BuildResult<ErrorType, Core.ObjectInfo> Evaluate(Core.ObjectInfo @object) =>
            _evaluator.Evaluate(@object);

        public Core.BuildResult<ErrorType, Core.Expression?> Evaluate(Core.Expression expression) =>
            _evaluator.Evaluate(expression);

        public void BeginDocument(Core.DocumentInfo document)
        {
            if (!_documentScopes.TryGetValue(document, out SearchScope scope))
            {
                scope = Scope.EnterScope();
                _documentScopes.Add(document, scope);
            }
            Scope.InsertScope(scope);
            //Console.WriteLine($"Begin document: {document}");
            //throw new NotImplementedException();
        }

        public void EndDocument()
        {
            Scope.ExitScope();
            //throw new NotImplementedException();
        }
    }
}
