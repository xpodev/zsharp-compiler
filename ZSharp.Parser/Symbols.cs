using Pidgin;
using ZSharp.Core;
using static ZSharp.Parser.ParserState;

namespace ZSharp.Parser
{
    public static class Symbols
    {
        public static Parser<char, FileInfo> Symbol(char symbol) => Syntax.Char(symbol);

        // the reason these 2 are special is because we don't want to remove whitespace
        // after these (for example, when you create a string or a char literal)
        public static readonly Parser<char, FileInfo> DoubleQuotes = 
            Pidgin.Parser
            .Char('"')
            .Select(c => GetInfo(c.ToString()));

        public static readonly Parser<char, FileInfo> SingleQuote =
            Pidgin.Parser
            .Char('\'')
            .Select(c => GetInfo(c.ToString()));

        public static readonly Parser<char, FileInfo> LCurvyBracket = Symbol('(');
        public static readonly Parser<char, FileInfo> RCurvyBracket = Symbol(')');
    }
}
