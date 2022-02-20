using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public class TypedName : NamedItem
    {
        public IType Type { get; }

        public TypedName(string name, IType type) : base(name)
        {
            Type = type;
        }

        [OperatorOverload(":")]
        public static TypedName CreateTypedName(Identifier id, IType type)
        {
            return new TypedName(id.Name, type);
        }
    }
}
