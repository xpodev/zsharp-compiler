namespace ZSharp.Core
{
    public class UnaryExpression : Expression
    {
        public FunctionName Operator { get; set; }

        public ObjectInfo Operand { get; set; }

        public bool IsPrefix { get; set; }

        public UnaryExpression(ObjectInfo operand, FunctionName @operator, bool isPrefix = true)
        {
            Operand = operand;
            Operator = @operator;
            IsPrefix = isPrefix;
        }
    }
}
