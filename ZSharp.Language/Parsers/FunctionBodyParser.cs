using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSharp.Parser;

namespace ZSharp.Language
{
    internal class FunctionBodyParser : ExtensibleParser<FunctionBody, DocumentObject>
    {
        public FunctionBodyParser() : base("FunctionBody", "<ZSharp>") { }

        internal void Build(Parser.Parser parser)
        {
            //Parser<char, IEnumerable<DocumentObject>> itemParser = Pidgin.Parser.OneOf(_ex)

            Parser =
                from expr in Pidgin.Parser.OneOf(
                    parser.Document.Symbols.Symbol("=>").Then(parser.Document.Expression.Parser).UpCast(),
                    parser.Document.Expression.Parser.ManyInside(BracketType.Curly).UpCast()
                    )
                select new FunctionBody()
                {
                    Code = expr
                };
        }
    }
}
