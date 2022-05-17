using ZSharp.Parser;

namespace ZSharp.Language
{
    internal class FunctionParser : CustomParser<ModifiedObject<FunctionNode>>
    {
        public FunctionParser() : base("Function", "<ZSharp>")
        {
            
        }
        
        public FunctionParser Build(Parser.Parser parser, ExtensionContext ctx)
        {
            Parser = (
                from name in parser.Document.Identifier.Parser
                from generic in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Comma).Parenthesized(BracketType.Angle).BeforeWhitespace().Optional()
                from parameters in ctx.GetSingleton<TypedItemParser>().Parser.WithObjectInfo().Separated(parser.Document.Symbols.Comma).Parenthesized().BeforeWhitespace().Optional()
                from type in parser.Document.Symbols.Colon.Then(ctx.GetSingleton<TypeParser>().Parser.WithObjectInfo()).Optional()
                from body in parser.Document.CreateParser(ctx.GetSingleton<FunctionBodyParser>().Parser)
                select new FunctionNode()
                {
                    Name = name,
                    TypeParameters = generic.HasValue ? new(generic.Value) : new(),
                    Parameters = parameters.HasValue ? new(parameters.Value) : new(),
                    ReturnType = type.GetValueOrDefault(new NodeInfo<TypeNode>(new(), TypeNode.Infer)),
                    Body = body
                }.Create())
                .WithPrefixKeyword("func")
                .WithPrefixModifiers();

            parser.Document.AddExtension(this);
            return this;
        }
    }
}
