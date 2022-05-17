namespace ZSharp.Language.Parsers
{
    internal class ExpressionBlockParser : ExtensibleParser<Collection<Expression>, Expression>
    {
        public ExpressionBlockParser() : base("ExpressionBlock") { }
    }
}
