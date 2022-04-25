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

            foreach (string file in files) Engine.AddDocument(new Core.DocumentInfo(file));

            List<ObjectBuildResult> nodes = new(), target = new();

            foreach (string file in files)
            {
                nodes.AddRange(parser.ParseFile(file).Select(o => new Core.BuildResult<ErrorType, Core.NodeInfo>(o)));
            }

            target.Capacity = nodes.Count;

            foreach (Core.INodeProcessor processor in Engine.GetNodeProcessors())
            {
                processor.PreProcess();

                target.AddRange(processor.Process(nodes));

                processor.PostProcess();
                
                (nodes, target) = (target, nodes);

                target.Clear();
            }

            // convert nodes to objects
            List<Core.BuildResult<ErrorType, Core.Object>> source =
                (
                from item in 
                    from node in nodes
                    select node.Cast(
                        info => info.Object.GetCompilerObject()
                        )
                where item.HasValue
                select item).ToList()
                , result = new();
            
            foreach (Core.IObjectProcessor processor in Engine.GetObjectProcessors())
            {
                processor.PreProcess();

                result.AddRange(processor.Process(source));

                processor.PostProcess();

                (source, result) = (result, source);
                
                result.Clear();
            }

            result.ForEach(LogErrors);
        }

        public void FinishCompilation()
        {
            Engine.FinishCompilation(Options.OutputPath);
        }
        
        private static void LogErrors(Core.BuildResult<ErrorType, Core.Object> result)
        {
            result.Errors.ForEach(error => 
                Console.WriteLine(
                    $"{Path.GetFullPath(result.Value.Node.FileInfo.Document.Path)}" +
                    $"{error}"
                    )
                );
        }
    }
}
