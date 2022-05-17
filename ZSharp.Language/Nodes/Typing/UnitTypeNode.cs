using System;

namespace ZSharp.Language
{
    public record class UnitTypeNode : TupleType
    {
        public UnitTypeNode() : base(Array.Empty<NodeInfo<TypeNode>>())
        {
        }

        public override TypeNameNode AsTypeName()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "()";
        }
    }
}
