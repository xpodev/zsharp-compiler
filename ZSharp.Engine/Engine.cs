using System;
using System.Collections;
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

        private readonly Parser.Parser _parser = new();

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
            }
        }

        public void FinishCompilation(string path)
        {
            throw new NotImplementedException();
        }

        public IParser GetParser()
        {
            return _parser;
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
