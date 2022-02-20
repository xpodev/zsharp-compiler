namespace ZSharp.Parser
{
    public class IdentifierParser
    {
        public Parser<char, Identifier> Identifier { get; }

        public Parser<char, ObjectInfo<Identifier>> Parser { get; }

        internal IdentifierParser(DocumentParser doc)
        {
            Parser<char, char> firstCharacter, character;

            firstCharacter = CIOneOf("ABCDEFGHIJKLMNOPQRSTUVWXYZ_");
            character = firstCharacter.Or(OneOf("0123456789\'"));

            Identifier = Map((c, cs) => new Identifier(c + cs), firstCharacter, character.ManyString());
            Parser = doc.CreateParser(Identifier);
        }
    }
}
