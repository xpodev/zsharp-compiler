namespace ZSharp.Language.Extensions
{
    internal class UsingStatementExtension : ILanguageExtension
    {
        private readonly UsingStatementParser usingStatementParser = new();

        public void Initialize(Parser.Parser parser)
        {
            usingStatementParser.Build(parser);
            parser.Document.AddExtension(usingStatementParser);
        }
    }
}
