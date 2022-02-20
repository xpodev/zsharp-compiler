namespace ZSharp.Parser
{
    public class TermParser
    {
        internal Parser<char, ObjectInfo<Expression>> Parser { get; }

        internal TermParser(DocumentParser doc)
        {
            Parser = OneOf(
                doc.Literal.Parser.Select(v => v.With<Expression>()),
                doc.Identifier.Parser.Select(v => v.With<Expression>())
                );
        }
    }
}
