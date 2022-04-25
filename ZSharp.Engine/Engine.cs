using System;
using System.Collections.Generic;
using System.Reflection;
using ZSharp.Core;
using ZSharp.Parser.Extensibility;

[assembly: ZSharp.OldCore.LanguageEngine(typeof(ZSharp.Engine.Engine))]

namespace ZSharp.Engine
{
    public class Engine : ILanguageEngine
    {
        private readonly List<ILanguageExtension> _extensions = new();

        private readonly Parser.Parser _parser = new();

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
            foreach (Type type in assembly.DefinedTypes)
            {
                if (type.IsAssignableTo(typeof(ILanguageExtension)))
                {
                    if (type.GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>()) is not ILanguageExtension extension)
                        throw new Exception();
                    _extensions.Add(extension);
                }

                string @namespace = type.Namespace;
                IContext ns = Context;
                if (@namespace is not null)
                {
                    foreach (string part in @namespace.Split('.'))
                    {
                        IContext nested = ns.GetObject(part) as IContext;
                        if (nested is null)
                        {
                            Namespace newNS = new Namespace(part);
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
                IContext ns = Context;
                if (@namespace is not null)
                {
                    foreach (string part in @namespace.Split('.'))
                    {
                        IContext nested = ns.GetObject(part) as IContext;
                        if (nested is null)
                        {
                            Namespace newNS = new Namespace(part);
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
            yield return new DelegateProcessor<IContextPreparationItem>(this);
            yield return new ModifierProcessor(this);
            yield return new CodeGenerator(this);
            yield break;
        }

        public void Setup()
        {
            foreach (ILanguageExtension extension in _extensions)
            {
                extension.Initialize(_parser);
            }

            _parser.Build();
        }
    }
}
