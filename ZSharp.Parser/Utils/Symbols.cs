namespace ZSharp.Parser
{
    using Symbol = Parser<char, Unit>;

    public class Symbols
    {
        public Symbol Comma { get; }
        public Symbol Colon { get; }

        public Symbol LParen { get; }

        public Symbol RParen { get; }

        #region Brackets

        public Symbol LAngleBracket { get; }
        public Symbol RAngleBracket { get; }
        public Symbol LCurlyBracket { get; }
        public Symbol RCurlyBracket { get; }
        public Symbol LCurvyBracket { get; }
        public Symbol RCurvyBracket { get; }
        public Symbol LSquareBracket { get; }
        public Symbol RSquareBracket { get; }

        #endregion

        internal Symbols()
        {
            Comma = Char(',');
            Colon = Char(':');

            LParen = Char('(');
            RParen = Char(')');

            LAngleBracket = Char('<');
            RAngleBracket = Char('>');

            LCurlyBracket = Char('{');
            RCurlyBracket = Char('}');

            LSquareBracket = Char('[');
            RSquareBracket = Char(']');

            LCurvyBracket = LParen;
            RCurvyBracket = RParen;
        }

        private static Symbol Char(char c) => Rec(() => Pidgin.Parser.Char(c).IgnoreResult().BeforeWhitespace());

        public Symbol Symbol(string symbol) => Rec(() => Pidgin.Parser.String(symbol).IgnoreResult().BeforeWhitespace());
    }
}
