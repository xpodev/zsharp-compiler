using System;

namespace ZSharp.Language
{
    public record class UnitType : TupleType
    {
        public UnitType() : base(Array.Empty<NodeInfo<Type>>())
        {
        }

        public override TypeName AsTypeName()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "()";
        }
    }
}
