using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;

namespace ZSharp.Compiler
{
    internal class Compiler
    {
        private static readonly string _exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public CLIOptions Options { get; set; }

        public Core.ILanguageEngine Engine { get; private set; }

        public Compiler(CLIOptions options)
        {
            Options = options;

            Assembly engineAssembly = Assembly.LoadFrom(Path.GetFullPath(Options.LanguageEngine, _exePath));
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
            // temporary. remove this when the engine can dynamically load itself
            Engine.AddAssemblyReference(Engine.GetType().Assembly);

            foreach (string path in Options.References)
            {
                // todo: if dll is not found, search in CWD
                string assemblyPath = Path.IsPathRooted(path) ? path : Path.GetFullPath(path, _exePath);
                Engine.AddAssemblyReference(assemblyPath);
            }

            Engine.Setup();
        }

        public void Compile(params string[] files) =>
            Compile((IEnumerable<string>)files);

        public void Compile(IEnumerable<string> files)
        {
            Core.IParser parser = Engine.GetParser();
            Core.IExpressionProcessor processor;

            List<Core.BuildResult<ErrorType, Core.ObjectInfo>> source = new(), target = new();

            List<Document> documents = new();

            foreach (string file in files)
            {
                using TextReader content = File.OpenText(file);

                Core.DocumentInfo document = new(file);
                parser.SetDocument(document);
                documents.Add(
                    new(
                        document,
                        new(
                            parser.Parse(content).Select(o => new Core.BuildResult<ErrorType, Core.ObjectInfo>(o))
                            )
                        )
                    );
            }

            // todo: replace with for-loop
            while ((processor = Engine.NextProcessor()) is not null)
            {
                processor.PreProcess();

                foreach (var info in documents)
                {
                    Engine.BeginDocument(info.DocumentInfo);

                    info.Objects = processor.Process(info.Objects);

                    Engine.EndDocument();
                }

                processor.PostProcess();
            }

            documents.ForEach(document => document.Objects.ForEach(LogErrors));
        }

        public void FinishCompilcation()
        {
            Engine.FinishCompilation(Options.OutputPath);
        }

        private static void LogErrors(Core.BuildResult<ErrorType, Core.ObjectInfo> result)
        {
            Core.ObjectInfo value = result.Value;

            result.Errors.ForEach(error => 
                Console.WriteLine(
                    $"{Path.GetFullPath(value.FileInfo.Document.Path)}" +
                    //$"({value.FileInfo.StartLine}, {value.FileInfo.StartColumn}): " +
                    $"{error}"
                    )
                );
        }
    }
}
