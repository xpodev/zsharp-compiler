using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSharp.Language.Parsers
{
    internal class ExpressionBlockParser : ExtensibleParser<Collection<Expression>, Expression>
    {
        public ExpressionBlockParser() : base("ExpressionBlock", "<ZSharp>") { }
    }
}
