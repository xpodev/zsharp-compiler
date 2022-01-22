namespace ZSharp.Core
{
    public class OperatorName : FunctionName
    {
        public const string KeywordName = "operator";

        public string Operator { get; set; }

        public OperatorName(string @operator) : base(string.Concat(KeywordName, @operator))
        {
            Operator = @operator;
        }
    }
}
