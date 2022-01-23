using ZSharp.Core;

namespace ZSharp.Engine
{
    public class CollectionOperators
    {
        [OperatorOverload(",")]
        public static ObjectInfo CreateCollection(ObjectInfo a, ObjectInfo b)
        {
            return new(FileInfo.Combine(a.FileInfo, b.FileInfo), a.Expression as Collection ?? new(a, b));
        }
    }
}
