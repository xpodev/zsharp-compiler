using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

[assembly: ZSharp.Core.LanguageEngine(typeof(ZSharp.Engine.Context))]

namespace ZSharp.Engine
{
    public class Context : Core.ILanguageEngine<string>
    {
        private interface IDocumentObject : IGenericCompilable<IDocumentObject> { }

        public static Context CurrentContext { get; private set; }

        public TypeSystem TypeSystem { get; private set; }

        public readonly SRFModule Module;

        private int _currentPass = 0;

        private readonly Evaluator _evaluator;

        public readonly ProjectScope Scope = new();

        private readonly List<Core.IExpressionProcessor<string>> _processors = new();

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

            GenericProcessor<ISRFCompilable> srfProcess;
            _processors.AddRange(new Core.IExpressionProcessor<string>[]
            {
                //new GenericProcessor<IDocumentObject>(this),
                new DocumentProcessor(this),
                //_evaluator,
                new ObjectDesciptorProcessor(this),
                new GenericProcessor<IBuildable>(this),
                new DependencyFinder(this),
                new GenericProcessor<ISRFResolvable>(this),
                srfProcess = new GenericProcessor<ISRFCompilable>(this),
                new GenericProcessor<IResolvable>(this),
                new GenericProcessor<ICompilable>(this),
            });
            if (srfProcess is not null)
                srfProcess.OnPostProcess += () =>
                    Module.SRF.CreateGlobalFunctions();
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
                            Scope.GlobalScope.AddItem(new SRFFunctionOverload(new FunctionReference(method)), $"kw_{kw.Keyword}");
                        }
                        else if (method.GetCustomAttribute<SurroundingOperatorOverloadAttribute>() is SurroundingOperatorOverloadAttribute sur)
                        {
                            string name = sur.Operator;
                            name = sur.IsPrefix ? name + '_' : '_' + name;
                            Scope.GlobalScope.AddItem(new SRFFunctionOverload(new FunctionReference(method)), name);
                        }
                        else if (method.GetCustomAttribute<OperatorOverloadAttribute>() is OperatorOverloadAttribute op)
                        {
                            Scope.GlobalScope.AddItem(new SRFFunctionOverload(new FunctionReference(method)), $"_{op.Operator}");
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
                        method.DeclaringType is null ? new FunctionReference(method) : new MethodReference(method)
                        ));
                }
            }
        }

        public void FinishCompilation(string path)
        {
            Module.MC.Name = System.IO.Path.GetFileNameWithoutExtension(path);
            string dir = Path.GetDirectoryName(Path.GetFullPath(path));
            Module.MC.Write(path);

            foreach (AssemblyNameReference reference in Module.MC.AssemblyReferences)
            {
                var assembly = Module.MC.AssemblyResolver.Resolve(reference);
                string referencePath = Path.GetFullPath(Path.GetFileName(assembly.MainModule.FileName), dir);
                if (!File.Exists(referencePath))
                    File.Copy(assembly.MainModule.FileName, referencePath);
            }
        }

        public Core.IExpressionProcessor<string> NextProcessor()
        {
            if (_currentPass >= _processors.Count) return null;
            return _processors[_currentPass++];
        }

        public Core.Result<string, Core.ObjectInfo> Evaluate(Core.ObjectInfo @object) =>
            _evaluator.Evaluate(@object);

        public Core.Result<string, Core.Expression> Evaluate(Core.Expression expression) =>
            _evaluator.Evaluate(expression);

        public void BeginDocument(Core.DocumentInfo document)
        {
            //Console.WriteLine($"Begin document: {document}");
            //throw new NotImplementedException();
        }

        public void EndDocument()
        {
            //throw new NotImplementedException();
        }
    }
}
