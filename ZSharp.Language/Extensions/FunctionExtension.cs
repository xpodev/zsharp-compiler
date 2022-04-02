namespace ZSharp.Language.Extensions
{
    internal class FunctionExtension : ILanguageExtension
    {
        private readonly FunctionParser functionParser = new();

        public void Initialize(Parser.Parser parser)
        {
            functionParser.Build(parser);
            parser.Document.AddExtension(functionParser);
        }
    }
}
