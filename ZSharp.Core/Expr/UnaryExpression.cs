namespace ZSharp.Core
{
    public class UnaryExpression : Expression
    {
        public string Operator { get; set; }

        public ObjectInfo Operand { get; set; }

        public bool IsPrefix { get; set; }

        public UnaryExpression(ObjectInfo operand, string @operator, bool isPrefix = true)
        {
            Operand = operand;
            Operator = @operator;
            IsPrefix = isPrefix;
        }
    }
}
