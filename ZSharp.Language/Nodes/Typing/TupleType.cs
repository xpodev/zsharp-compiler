using System;
using System.Collections.Generic;

namespace ZSharp.Language
{
    public record class TupleType(IEnumerable<NodeInfo<Type>> Types) : Type
    {
        public override TypeName AsTypeName()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"({string.Join(", ", Types)})";
        }
    }
}
