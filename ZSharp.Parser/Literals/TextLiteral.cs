namespace ZSharp.Parser
{
    public class TextLiteral
    {
        public Parser<char, NodeInfo<Literal<string>>> String { get; }

        public Parser<char, NodeInfo<Literal<char>>> Char { get; }

        internal Parser<char, NodeInfo<Literal>> Parser { get; }

        internal TextLiteral(DocumentParser doc)
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

            Char = doc.CreateParser(
                character.Or(Any).Between(singleQuote).Select(c => new Literal<char>(c))
                );

            String = doc.CreateParser(
                character.Or(AnyCharExcept('\"')).ManyString().Between(doubleQuotes).Select(s => new Literal<string>(s))
                );

            Parser = OneOf(Char.Cast<NodeInfo<Literal>>(), String.Cast<NodeInfo<Literal>>());
        }
    }
}
