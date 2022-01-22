namespace ZSharp.Core
{
    public class BinaryExpression : Expression
    {
        public ObjectInfo Left { get; set; }

        public ObjectInfo Right { get; set; }

        public FunctionName Operator { get; set; }

        public BinaryExpression(ObjectInfo left, ObjectInfo right, FunctionName @operator)
        {
            Left = left;
            Right = right;
            Operator = @operator;
        }
    }
}
