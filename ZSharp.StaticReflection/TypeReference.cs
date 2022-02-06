using System;

namespace ZSharp.SR
{
    public class TypeReference
        : NamedItem
        , IType
    {
        protected internal readonly Type _ref;

        public TypeReference DeclaringType { get; }

        IType IMember.DeclaringType => DeclaringType;

        public TypeReference(Type type, TypeReference owner) : base(type.Name)
        {
            _ref = type;
            DeclaringType = owner;
        }
    }
}
