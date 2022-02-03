using Pidgin;
using static Pidgin.Parser;

namespace ZSharp.Parser
{
    public static class Syntax
    {
        private static readonly Parser<char, Unit> Whitespace = 
            Pidgin.Parser
            .Whitespace
            .IgnoreResult()
            .Or(EndOfLine.IgnoreResult());

        public static readonly Parser<char, Unit> Whitespaces =
            Whitespace
            .Many()
            .IgnoreResult();

        public static Parser<char, Unit> Char(char c) =>
            Pidgin.Parser
            .Char(c)
            .Then(Whitespaces);

        public static Parser<char, Unit> String(string s) =>
            Pidgin.Parser
            .String(s)
            .Then(Whitespaces);
    }
}
