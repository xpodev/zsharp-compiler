using Pidgin;
using static Pidgin.Parser;

namespace ZSharp.Parser
{
    public static class Identifier
    {
        public static readonly Parser<char, char> Underscore = Char('_');

        private static readonly Parser<char, char> FirstCharacter = OneOf(Letter, Underscore);
        
        private static readonly Parser<char, char> IdentifierCharacter = OneOf(FirstCharacter, Digit);
        private static readonly Parser<char, string> IdentifierCharacters = IdentifierCharacter.ManyString();

        private static readonly Parser<char, Unit> NonIdentifierCharacter = Not(IdentifierCharacters);
        public static readonly Parser<char, Unit> NonIdentifierCharacters = NonIdentifierCharacter.Many().IgnoreResult();

        private static readonly Parser<char, string> Name =
            from c in FirstCharacter
            from cs in IdentifierCharacters
            select string.Concat(c, cs);

        internal static readonly Parser<char, string> Parser =
            from name in Name.Before(Syntax.Whitespaces)
            select name;
    }
}
