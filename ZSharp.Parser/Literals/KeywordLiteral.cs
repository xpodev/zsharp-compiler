namespace ZSharp.Parser
{
    public class KeywordLiteral
    {
        const string NullKeyword = "null";
        const string TrueKeyword = "true";
        const string FalseKeyword = "false";

        public static Literal<object> Null { get; } = new(null);

        public static Literal<bool> True { get; } = new(true);

        public static Literal<bool> False { get; } = new(false);

        public Parser<char, NodeInfo<Literal<object>>> NullParser { get; }

        public Parser<char, NodeInfo<Literal<bool>>> TrueParser { get; }

        public Parser<char, NodeInfo<Literal<bool>>> FalseParser { get; }

        public Parser<char, NodeInfo<Literal<bool>>> Boolean { get; }

        public Parser<char, NodeInfo<Literal>> Parser { get; }

        internal KeywordLiteral(DocumentParser doc)
        {
            NullParser = doc.CreateParser(doc.Identifier.Parser.Assert(s => s.Object.Name == NullKeyword).ThenReturn(Null));
            TrueParser = doc.CreateParser(doc.Identifier.Parser.Assert(s => s.Object.Name == TrueKeyword).ThenReturn(True));
            FalseParser = doc.CreateParser(doc.Identifier.Parser.Assert(s => s.Object.Name == FalseKeyword).ThenReturn(False));

            Parser = OneOf(
                NullParser.Select(v => v.With<Literal>()),
                TrueParser.Select(v => v.With<Literal>()),
                FalseParser.Select(v => v.With<Literal>())
                );
        }
    }
}
