using Pidgin;

namespace ZSharp.Parser
{
    public static class Symbols
    {
        public static Parser<char, Unit> Symbol(char symbol) => Syntax.Char(symbol);

        // the reason these 2 are special is because we don't want to remove whitespace
        // after these (for example, when you create a string or a char literal)
        public static readonly Parser<char, Unit> DoubleQuotes = 
            Pidgin.Parser
            .Char('"')
            .IgnoreResult();

        public static readonly Parser<char, Unit> SingleQuote =
            Pidgin.Parser
            .Char('\'')
            .IgnoreResult();

        public static readonly Parser<char, Unit> LCurvyBracket = Symbol('(');
        public static readonly Parser<char, Unit> RCurvyBracket = Symbol(')');
    }
}
