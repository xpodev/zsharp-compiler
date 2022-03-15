namespace ZSharp.Core
{
    public class BinaryExpression : Expression
    {
        public NodeInfo<Expression> Left { get; set; }

        public NodeInfo<Expression> Right { get; set; }

        public string Operator { get; set; }

        public BinaryExpression(NodeInfo<Expression> left, NodeInfo<Expression> right, string @operator)
        {
            Left = left;
            Right = right;
            Operator = @operator;
        }
    }
}
