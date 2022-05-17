using System;
using System.Collections.Generic;

namespace ZSharp.Language
{
    public record class TupleType(IEnumerable<NodeInfo<TypeNode>> Types) : TypeNode
    {
        public override TypeNameNode AsTypeName()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"({string.Join(", ", Types)})";
        }
    }
}
