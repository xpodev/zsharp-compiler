namespace ZSharp.Parser
{
    internal class TextLiteral
    {
        internal Parser<char, Core.Literal> String { get; }

        internal Parser<char, Core.Literal> Char { get; }

        internal TextLiteral()
        {
            Parser<char, Unit> doubleQuotes, singleQuote, backslash;
            Parser<char, char> character;
            doubleQuotes = Char('\"').IgnoreResult();
            singleQuote = Char('\'').IgnoreResult();

            backslash = Char('\\').IgnoreResult();
            character = backslash.Then(Any.Select(c => 
                c switch
                {
                    '\\' => '\\',
                    'n' => '\n',
                    'r' => '\r',
                    _ => c
                }
            ));

            Char = 
                from c in character.Or(Any).Between(singleQuote)
                select new Core.Literal(c);

            String = 
                from s in character.Or(AnyCharExcept('\"')).ManyString().Between(doubleQuotes)
                select new Core.Literal(s);
        }
    }
}
