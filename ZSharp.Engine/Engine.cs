using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZSharp.Core;
using ZSharp.Parser.Extensibility;

[assembly: ZSharp.OldCore.LanguageEngine(typeof(ZSharp.Engine.Engine))]

namespace ZSharp.Engine
{
    public class Engine : ILanguageEngine
    {
        private readonly List<ILanguageExtension> _extensions = new();
        private readonly ExtensionContext _extensionContext = new();
        private readonly Parser.Parser _parser = new();

        private readonly List<Assembly> _assemblyReferences = new();

        public ProjectScope Context { get; } = new();

        public ExpressionEvaluator Evaluator { get; }

        public Engine()
        {
            Evaluator = new ExpressionEvaluator(Context);
        }

        public void AddAssemblyReference(string path)
        {
            AddAssemblyReference(Assembly.LoadFrom(path));
        }

        public void AddAssemblyReference(Assembly assembly)
        {
            _assemblyReferences.Add(assembly);
        }

        public void ImportAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.DefinedTypes)
            {
                if (type.IsAssignableTo(typeof(ILanguageExtension)) && !type.IsInterface)
                {
                    if (type.GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>()) is not ILanguageExtension extension)
                        throw new Exception();
                    _extensions.Add(extension);
                }

                string @namespace = type.Namespace;
                IScope ns = Context;
                if (@namespace is not null)
                {
                    foreach (string part in @namespace.Split('.'))
                    {
                        if (ns.GetObject(part) is not IScope nested)
                        {
                            Namespace newNS = new(part);
                            ns.TryAddObject(newNS);
                            ns = newNS;
                        }
                        else ns = nested;
                    }
                }

                ns.TryAddObject(new TypeReference(Context.Module.GetTypeReference(type), new(type, null)));
            }

            foreach (Type type in assembly.GetForwardedTypes())
            {
                if (type.IsAssignableTo(typeof(ILanguageExtension)))
                {
                    if (type.GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>()) is not ILanguageExtension extension)
                        throw new Exception();
                    _extensions.Add(extension);
                }

                string @namespace = type.Namespace;
                IScope ns = Context;
                if (@namespace is not null)
                {
                    foreach (string part in @namespace.Split('.'))
                    {
                        if (ns.GetObject(part) is not IScope nested)
                        {
                            Namespace newNS = new(part);
                            ns.TryAddObject(newNS);
                            ns = newNS;
                        }
                        else ns = nested;
                    }
                }

                ns.TryAddObject(new TypeReference(Context.Module.GetTypeReference(type), new(type, null)));
            }
        }

        public void AddDocument(DocumentInfo document) => Context._documents.Add(document.FileName, new());

        public void FinishCompilation(string path)
        {
            Context.Module.Write(path);
            //throw new NotImplementedException();
        }

        public IParser GetParser()
        {
            return _parser;
        }

        public IEnumerable<IObjectProcessor> GetObjectProcessors()
        {
            yield return null;
        }

        public IEnumerable<INodeProcessor> GetNodeProcessors()
        {
            yield return new Processor<IContextPreparationItem>(this);
            yield return new ModifierProcessor(this);
            yield return new CodeGenerator(this);
            yield break;
        }

        public void Setup()
        {
            DependencyGraph<string> graph = new();

            foreach (Assembly assembly in _assemblyReferences)
            {
                graph.AddDependencies(assembly.FullName, assembly.GetReferencedAssemblies().Select(a =>
                {
                    if (!graph.Contains(a.FullName))
                        graph.AddDependencies(a.FullName);
                    return a.FullName;
                }));
            }

            foreach (IEnumerable<string> names in graph.GetDependencyOrder())
            {
                foreach (string name in names)
                {
                    Assembly assembly = Assembly.Load(name);
                    ImportAssembly(assembly);
                }
            }
            
            foreach (ILanguageExtension extension in _extensions)
            {
                extension.Initialize(_parser, _extensionContext);
            }

            _parser.Build();
        }
    }
}
