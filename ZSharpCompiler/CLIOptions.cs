using CommandLine;

namespace ZSharp.Compiler
{
    internal class CLIOptions
    {
        [Value(0, HelpText = "The paths of the files to compile. If this is a directory, all .zs files will be compiled", Required = true, Min = 1)]
        public System.Collections.Generic.IEnumerable<string> Files { get; set; }

        [Option('o', "out", HelpText = "Output path of the assembly")]
        public string OutputPath { get; set; }

        [Option('t', "type")]
        public OldCore.ModuleKind Kind { get; set; }

        [Option('v', "version")]
        public string Version { get; set; }

        [Option('r', "ref", Separator = ';')]
        public System.Collections.Generic.IEnumerable<string> References { get; set; }

        //[Option("no-srf", Default = false, HelpText = "Whether or not to load Z# SRF standard library")]
        //public bool NoSRF { get; set; }

        [Option('e', "engine", Default = "ZSharp.Engine.dll", HelpText = "The language engine to load (.dll file)")]
        public string LanguageEngine { get; set; }

        //[Option("sdk", Default = "./SDK/", HelpText = "The path to the .NET SDK")]
        //public string SDKPath { get; set; }
    }
}
