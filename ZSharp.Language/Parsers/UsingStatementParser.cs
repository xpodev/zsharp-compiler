using ZSharp.Parser;
using static Pidgin.Parser;

namespace ZSharp.Language
{
    internal class UsingStatementParser : CustomParser<UsingStatement>
    {
        public UsingStatementParser() : base("UsingStatement", "<ZSharp>")
        {

        }

        public UsingStatementParser Build(Parser.Parser parser)
        {
            // todo: make resursive
            Parser = OneOf(
                from fqn in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Dot)
                select new UsingNamespaceStatement(new FullyQualifiedName(fqn)) as UsingStatement
                ).WithPrefixKeyword("using").Before(parser.Document.Symbols.Semicolon);

            parser.Document.AddExtension(this);
            return this;
        }
    }
}
