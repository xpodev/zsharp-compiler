using Pidgin;
using static Pidgin.Parser;
using static ZSharp.Parser.ParserState;

namespace ZSharp.Parser
{
    public static class Syntax
    {
        private static readonly Parser<char, string> Whitespace = 
            Pidgin.Parser
            .Whitespace
            .Select(c => c.ToString())
            .Or(EndOfLine);

        public static readonly Parser<char, Core.FileInfo> Whitespaces =
            Whitespace
            .ManyString()
            .Select(GetInfo);

        public static Parser<char, Core.FileInfo> Char(char c) =>
            Pidgin.Parser
            .Char(c)
            .Select(c => GetInfo(c.ToString()))
            .Before(Whitespaces);

        public static Parser<char, Core.FileInfo> String(string s) =>
            Pidgin.Parser
            .String(s)
            .Select(GetInfo)
            .Before(Whitespaces);
    }
}
