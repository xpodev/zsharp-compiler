using ZSharp.Core;

namespace ZSharp.Engine
{
    public class CollectionOperators
    {
        [OperatorOverload(",")]
        public static Collection CreateCollection(Expression a, Expression b)
        {
            return new(a, b);
        }

        [OperatorOverload(",")]
        public static Collection CreateCollection(Collection a, Expression b)
        {
            a.Items.Add(b);
            return a;
        }
    }
}
