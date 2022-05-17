using ZSharp.Parser;

namespace ZSharp.Language
{
    internal class FunctionBodyParser : ExtensibleParser<FunctionBodyNode, Node>
    {
        public FunctionBodyParser() : base("FunctionBody", "<ZSharp>") { }

        internal FunctionBodyParser Build(Parser.Parser parser)
        {
            //Parser<char, IEnumerable<Node>> itemParser = Pidgin.Parser.OneOf(_ex)

            Parser =
                from expr in Pidgin.Parser.OneOf(
                    parser.Document.Symbols.Symbol("=>").Then(parser.Document.Expression.Parser).UpCast(),
                    parser.Document.Expression.Parser.ManyInside(BracketType.Curly).UpCast()
                    )
                select new FunctionBodyNode()
                {
                    Code = expr
                };

            return this;
        }
    }
}
