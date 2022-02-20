namespace ZSharp.Parser
{
    public class LiteralParser
    {
        public TextLiteral Text { get; }

        public NumberLiteral Number { get; }

        public KeywordLiteral Keyword { get; }

        internal Parser<char, ObjectInfo<Literal>> Parser { get; }

        internal LiteralParser(DocumentParser doc)
        {
            Text = new(doc);
            Number = new(doc);
            Keyword = new(doc);

            Parser = OneOf(
                Text.Parser,
                Number.Parser,
                Keyword.Parser
                );
        }
    }
}
