namespace ZSharp.Core
{
    public class BinaryExpression : Expression
    {
        public ObjectInfo<Expression> Left { get; set; }

        public ObjectInfo<Expression> Right { get; set; }

        public string Operator { get; set; }

        public BinaryExpression(ObjectInfo<Expression> left, ObjectInfo<Expression> right, string @operator)
        {
            Left = left;
            Right = right;
            Operator = @operator;
        }
    }
}
