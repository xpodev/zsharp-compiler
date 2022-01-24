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
        private static readonly string _exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public CLIOptions Options { get; set; }

        public Core.ILanguageEngine<string> Engine { get; private set; }

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
                    as Core.ILanguageEngine<string>;
            }

            if (Engine is null) throw new Exception($"Language engine could not be initialized");
        }

        public void Setup()
        {
            //Parser.ParserState.Reset(null);

            // temporary. remove this when the engine can dynamically load itself
            Engine.AddAssemblyReference(Engine.GetType().Assembly);

            //string compilerRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //foreach (string path in Directory.EnumerateFiles(Path.GetFullPath(Options.SDKPath, compilerRoot), "*.dll"))
            //{
            //    Engine.AddAssemblyReference(path);
            //}

            foreach (string path in Options.References)
            {
                // todo: if dll is not found, search in CWD
                string assemblyPath = Path.IsPathRooted(path) ? path : Path.GetFullPath(path, _exePath);
                Engine.AddAssemblyReference(assemblyPath);
            }

            //Engine.AddAssemblyReference(typeof(Type).Assembly);

            Engine.Setup();
        }

        public void Compile(params string[] files) =>
            Compile((IEnumerable<string>)files);

        public void Compile(IEnumerable<string> files)
        {
            Core.IExpressionProcessor<string> processor;

            List<(Core.DocumentInfo document, List<Core.Result<string, Core.ObjectInfo>> objects)> source = new(), target = new();

            foreach (string file in files)
            {
                using TextReader content = File.OpenText(file);

                Core.DocumentInfo document = new(file);
                Parser.ParserState.Reset(document);
                source.Add(
                    new(
                        document, 
                        new(
                            Parser.Expression.Single
                            .Many()
                            .ParseOrThrow(content)
                            .Select(o => new Core.Result<string, Core.ObjectInfo>(o))
                            )
                        )
                    );
            }

            while (source.Count > 0 && (processor = Engine.NextProcessor()) is not null)
            {
                processor.PreProcess();

                foreach (var (document, objects) in source)
                {
                    Engine.BeginDocument(document);

                    target.Add(
                        new(
                            document, 
                            new(
                                objects.Select(processor.Process)
                                )
                            )
                        );

                    Engine.EndDocument();
                }

                processor.PostProcess();

                source.Clear();
                Swap(ref source, ref target);
            }

            foreach ((Core.DocumentInfo _, List<Core.Result<string, Core.ObjectInfo>> results) item in source)
            {
                foreach (Core.Result<string, Core.ObjectInfo> result in item.results)
                {
                    LogError(result);
                }
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

        private static void LogError(Core.Result<string, Core.ObjectInfo> result)
        {
            if (result.IsSuccess) return;

            Console.WriteLine(
                $"{Path.GetFullPath(result.Object.FileInfo.Document.Path)}" +
                $"({result.Object.FileInfo.StartLine}, {result.Object.FileInfo.StartColumn}): " +
                $"{result.Error}");
        }
    }
}
