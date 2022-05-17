using Pidgin;

namespace ZSharp.Language
{
    internal class TypedItemParser
    {
        public Parser<char, TypedItemNode> Parser { get; private set; }

        internal TypedItemParser Build(Parser.Parser parser, ExtensionContext ctx)
        {
            Parser =
                from name in parser.Document.Identifier.Parser
                from type in parser.Document.Symbols.Colon.Then(ctx.GetSingleton<TypeParser>().Parser).Optional()
                select new TypedItemNode(name, new(new(), type.GetValueOrDefault(TypeNode.Infer)));

            return this;
        }
    }
}
