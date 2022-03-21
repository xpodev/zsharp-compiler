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
            if (engineAssembly.GetCustomAttribute<OldCore.LanguageEngineAttribute>() is OldCore.LanguageEngineAttribute engine)
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

            List<ObjectBuildResult> source = new(), target = new();

            foreach (string file in files)
            {
                source.AddRange(parser.ParseFile(file).Select(o => new Core.BuildResult<ErrorType, Core.NodeInfo>(o)));
            }

            target.Capacity = source.Count;

            foreach (Core.INodeProcessor processor in Engine.GetProcessors())
            {
                processor.PreProcess();

                target.AddRange(processor.Process(source));

                processor.PostProcess();

                (source, target) = (target, source);
            }

            source.ForEach(LogErrors);
        }

        public void FinishCompilation()
        {
            Engine.FinishCompilation(Options.OutputPath);
        }

        private static void LogErrors(Core.BuildResult<ErrorType, Core.NodeInfo> result)
        {
            Core.NodeInfo value = result.Value;

            result.Errors.ForEach(error => 
                Console.WriteLine(
                    $"{Path.GetFullPath(value.FileInfo.Document.Path)}" +
                    $"{error}"
                    )
                );
        }
    }
}
