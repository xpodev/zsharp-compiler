namespace ZSharp.Core
{
    public class BinaryExpression : Expression
    {
        public ObjectInfo Left { get; set; }

        public ObjectInfo Right { get; set; }

        public string Operator { get; set; }

        public BinaryExpression(ObjectInfo left, ObjectInfo right, string @operator)
        {
            Left = left;
            Right = right;
            Operator = @operator;
        }
    }
}
