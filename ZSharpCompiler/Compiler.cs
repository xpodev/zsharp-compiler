using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Pidgin;
using System.Linq;

namespace ZSharp.Compiler
{
    internal class Compiler
    {
        public CLIOptions Options { get; set; }

        public Core.ILanguageEngine Engine { get; private set; }

        public Compiler(CLIOptions options)
        {
            Options = options;

            return;

            Assembly engineAssembly = Assembly.LoadFrom(Path.GetFullPath(Options.LanguageEngine));
            if (engineAssembly.GetCustomAttribute<Core.LanguageEngineAttribute>() is Core.LanguageEngineAttribute engine)
            {
                Engine =
                    engine
                    .EngineType
                    .GetConstructor(Type.EmptyTypes)
                    .Invoke(Array.Empty<object>())
                    as Core.ILanguageEngine;
            }

            if (Engine is null) throw new Exception($"Language engine could not be initialized");
        }

        public void Setup()
        {
            return;

            // temporary. remove this when the engine can dynamically load itself
            Engine.AddAssemblyReference(Engine.GetType().Assembly);

            foreach (string path in Options.References)
            {
                Engine.AddAssemblyReference(Path.GetFullPath(path));
            }

            Engine.AddAssemblyReference(typeof(Type).Assembly);

            Engine.Setup();
        }

        public void Compile(params string[] files) =>
            Compile((IEnumerable<string>)files);

        public void Compile(IEnumerable<string> files)
        {
            Core.IExpressionProcessor processor;

            List<(Core.DocumentInfo document, List<Core.ObjectInfo> objects)> source = new();
            List<(Core.DocumentInfo document, List<Core.ObjectInfo> objects)> target = new();

            foreach (string file in files)
            {
                using TextReader content = File.OpenText(file);

                Core.DocumentInfo document = new(file);
                Parser.ParserState.Reset(document);
                source.Add(new(document, new(Parser.Expression.Single.Many().ParseOrThrow(content))));
            }

            while (source.Count > 0 && (processor = Engine.NextProcessor()) is not null)
            {
                processor.PreProcess();

                foreach (var info in source)
                {
                    Engine.BeginDocument(info.document);

                    foreach (var node in info.objects)
                    {
                        processor.Process(node);
                    }

                    Engine.EndDocument();
                }

                processor.PostProcess();

                source.Clear();
                Swap(ref source, ref target);
            }
        }

        public void FinishCompilcation()
        {
            Engine.FinishCompilation(Options.OutputPath);
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
