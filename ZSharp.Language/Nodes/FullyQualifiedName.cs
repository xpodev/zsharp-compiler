using System.Collections.Generic;
using System.Linq;

namespace ZSharp.Language
{
    public class FullyQualifiedName
    {
        public NodeInfo<Identifier>[] Parts { get; }

        public FullyQualifiedName(IEnumerable<NodeInfo<Identifier>> parts) : this(parts.ToArray()) { }

        public FullyQualifiedName(params NodeInfo<Identifier>[] parts)
        {
            Parts = parts;
        }

        public override string ToString()
        {
            return string.Join('.', Parts.Select(part => part.Object.Name));
        }
    }
}
