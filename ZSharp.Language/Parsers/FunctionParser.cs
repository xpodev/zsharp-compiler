using ZSharp.Parser;

namespace ZSharp.Language
{
    internal class FunctionParser : CustomParser<Function>
    {
        public FunctionBodyParser Body { get; } = new();

        public FunctionParser() : base("Function", "<ZSharp>")
        {
            
        }

        public void Build(Parser.Parser parser)
        {
            Body.Build(parser);

            Parser = 
                from name in parser.Document.Identifier.Parser
                from generic in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Comma).Parenthesized(BracketType.Angle).BeforeWhitespace().Optional()
                from parameters in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Comma).Parenthesized().BeforeWhitespace().Optional()
                from type in parser.Document.Symbols.Colon.Then(parser.Document.Expression.Parser).Optional()
                from body in parser.Document.CreateParser(Body.Parser)
                select new Function()
                {
                    Name = name,
                    GenericParameters = generic.HasValue ? new(generic.Value) : new(),
                    Parameters = parameters.HasValue ? new(parameters.Value) : new(),
                    Type = type.HasValue ? type.Value : new(new(), AutoType.Infer),
                    Body = body
                }.Create();
        }
    }
}
