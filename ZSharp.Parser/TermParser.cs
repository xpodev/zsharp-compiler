using Pidgin;
using ZSharp.Core;
using static Pidgin.Parser;

namespace ZSharp.Parser
{
    internal class TermParser
    {
        internal Parser<char, Identifier> Identifier { get; }

        internal Parser<char, ObjectInfo> Parser { get; }

        internal TermParser(DocumentParser p)
        {
            Parser<char, char> firstCharacter, character;

            firstCharacter = CIOneOf("ABCDEFGHIJKLMNOPQRSTUVWXYZ_\'");
            character = firstCharacter.Or(OneOf("0123456789"));

            Identifier = Map((c, cs) => new Identifier(c + cs), firstCharacter, character.ManyString());


            LiteralParser literal = new(p, this);

            Parser = literal.Parser.Or(p.CreateParser(Identifier));
        }
    }
}
