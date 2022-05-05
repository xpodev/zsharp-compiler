namespace ZSharp.Core
{
    public record class UnaryExpression(NodeInfo<Expression> Operand, string Operator) : Expression
    {
        public bool IsPrefix { get; set; }

        public UnaryExpression(NodeInfo<Expression> operand, string @operator, bool isPrefix = true)
            : this(operand, @operator)
        {
            Operand = operand;
            Operator = @operator;
            IsPrefix = isPrefix;
        }

        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
