namespace ZSharp.Parser
{
    internal class KeywordLiteral
    {
        const string NullKeyword = "null";
        const string TrueKeyword = "true";
        const string FalseKeyword = "false";

        internal Parser<char, Expression> Parser { get; }

        internal KeywordLiteral(TermParser term)
        {
            Parser = term.Identifier.Select<Expression>(
                id => id.Name switch
                {
                    NullKeyword => new Literal(null),
                    TrueKeyword => new Literal(true),
                    FalseKeyword => new Literal(false),
                    string s => new Identifier(s)
                });
        }
    }
}
