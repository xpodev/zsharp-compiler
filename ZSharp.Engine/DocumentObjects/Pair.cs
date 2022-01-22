using ZSharp.Core;

namespace ZSharp.Engine
{
    public class Pair : Expression
    {
        public Expression Left { get; }

        public Expression Right { get; }

        public Pair(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        //[OperatorOverload(":")]
        public static Pair CreatePair(Expression left, Expression right) => new(left, right);
    }
}
