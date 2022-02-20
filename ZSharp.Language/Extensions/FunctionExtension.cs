namespace ZSharp.Language.Extensions
{
    internal class FunctionExtension : ILanguageExtension
    {
        private readonly FunctionParser functionParser = new();

        public void Initialize(Parser.Parser parser)
        {
            functionParser.Build(parser);
            parser.Document.AddExtension(functionParser, new()
            {
                AllowBlockDefinition = true,
                BlockBracketType = Parser.BracketType.Curly,
                Keyword = "func",
                IsModifiable = true,
                AllowDefault = false
            });
        }
    }
}
