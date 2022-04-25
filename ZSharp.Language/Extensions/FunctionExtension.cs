namespace ZSharp.Language.Extensions
{
    internal class FunctionExtension : ILanguageExtension
    {
        private readonly TypeParser typeParser = new();
        private readonly FunctionParser functionParser;

        public FunctionExtension()
        {
            functionParser = new(typeParser);
        }

        public void Initialize(Parser.Parser parser)
        {
            typeParser.Build(parser);
            functionParser.Build(parser);
            parser.Document.AddExtension(functionParser);
        }
    }
}
