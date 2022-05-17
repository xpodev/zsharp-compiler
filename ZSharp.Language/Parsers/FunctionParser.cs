using ZSharp.Parser;

namespace ZSharp.Language
{
    internal class FunctionParser : CustomParser<ModifiedObject<FunctionNode>>
    {
        public FunctionParser() : base("Function")
        {
            
        }
        
        public FunctionParser Build(Parser.Parser parser, ExtensionContext ctx)
        {
            Parser = (
                from name in parser.Document.Identifier.Parser
                from generic in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Comma).Parenthesized(BracketType.Angle).BeforeWhitespace().Optional()
                from parameters in ctx.GetSingleton<TypedItemParser>().Parser.WithPrefixModifiers().WithObjectInfo().Separated(parser.Document.Symbols.Comma).Parenthesized().BeforeWhitespace().Optional()
                from type in parser.Document.Symbols.Colon.Then(ctx.GetSingleton<TypeParser>().Parser.WithObjectInfo()).Optional()
                from body in parser.Document.CreateParser(ctx.GetSingleton<FunctionBodyParser>().Parser)
                select new FunctionNode(
                    name, 
                    type.GetValueOrDefault(),
                    generic.HasValue ? new(generic.Value) : new(),
                    parameters.HasValue ? new(parameters.Value) : new(),
                    body
                ))
                .WithPrefixModifiers("func");

            parser.Document.AddExtension(this);
            return this;
        }
    }
}
