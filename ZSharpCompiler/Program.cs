using System.IO;
using CommandLine;

namespace ZSharp.Compiler
{
    class Program
    {
        static int Main(string[] args)
        {
            ParserResult<CLIOptions> options = CommandLine.Parser.Default.ParseArguments<CLIOptions>(args);
            if (options.Tag != ParserResultType.Parsed)
                return -1;

            options.WithParsed(Compile);

            //Compile(new()
            //{
            //    OutputPath = "source.exe",
            //    Files = new[] { "." },
            //    References = new[] { "./SDK/System.Console.dll" },
            //    LanguageEngine = "ZSharp.Engine.dll"
            //});

            return 0;
        }

        static void Compile(CLIOptions options)
        {
            Compiler compiler = new(options);

            compiler.Setup();

            System.Collections.Generic.List<string> files = new();

            foreach (string file in options.Files)
            {
                if (Directory.Exists(file))
                    files.AddRange(Directory.EnumerateFiles(file, "*.zs"));
                else
                    files.Add(file);
            }

            compiler.Compile(files);

            compiler.FinishCompilcation();
        }
    }
}
