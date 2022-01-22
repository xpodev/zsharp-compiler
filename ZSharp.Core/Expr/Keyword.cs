
namespace ZSharp.Core
{
    public class Keyword : Expression
    {
        public const string KeywordValue = "keyword";

        public string KeywordName { get; set; }

        public Expression SubExpression { get; set; }

        public Keyword(string keyword, Expression subExpression)
        {
            KeywordName = keyword;
            SubExpression = subExpression;
        }

        public Keyword(Expression subExpression) : this(KeywordValue, subExpression)
        {

        }

        public override string ToString()
        {
            return string.Concat(KeywordName, ' ', SubExpression);
        }
    }
}
