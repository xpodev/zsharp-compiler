using ZSharp.Engine;

namespace ZSharp.Engine
{
    public class Namespace : ScopeBase, INamedObject
    {
        public string Name { get; }

        public Namespace(string name)
        {
            Name = name;
        }
    }
}
