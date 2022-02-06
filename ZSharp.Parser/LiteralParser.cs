using Pidgin;
using static Pidgin.Parser;
using ZSharp.Core;

namespace ZSharp.Parser
{
    internal class LiteralParser
    {
        internal Parser<char, ObjectInfo> Parser;

        internal LiteralParser(DocumentParser p, TermParser t)
        {
            TextLiteral text = new();
            NumberLiteral number = new();
            KeywordLiteral keyword = new(t);

            Parser = OneOf(
                p.CreateParser(text.Char),
                p.CreateParser(text.String),
                p.CreateParser(number.Integer),
                p.CreateParser(number.Real),
                p.CreateParser(keyword.Parser)
                );
        }
    }
}
