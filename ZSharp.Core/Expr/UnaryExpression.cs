namespace ZSharp.Core
{
    public class UnaryExpression : Expression
    {
        public string Operator { get; set; }

        public NodeInfo Operand { get; set; }

        public bool IsPrefix { get; set; }

        public UnaryExpression(NodeInfo operand, string @operator, bool isPrefix = true)
        {
            Operand = operand;
            Operator = @operator;
            IsPrefix = isPrefix;
        }
    }
}
