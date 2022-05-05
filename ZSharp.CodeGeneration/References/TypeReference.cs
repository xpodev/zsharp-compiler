using System.Collections.Generic;

namespace ZSharp.CG
{
    public class TypeReference
        : NamedItem
        , IType
    {
        private static readonly Dictionary<MC.TypeReference, TypeReference> _cache = new();

        protected internal readonly MC.TypeReference _ref;

        public TypeReference DeclaringType { get; }

        public FieldReference[] Fields { get; }

        public TypeReference[] Interfaces { get; }

        public MethodReference[] Methods { get; }

        public Property[] Properties { get; }

        public TypeReference[] NestedTypes { get; }

        IType IMember.DeclaringType => DeclaringType;

        IReadOnlyList<IField> IType.Fields => Fields;

        IReadOnlyList<IType> IType.Interfaces => Interfaces;

        IReadOnlyList<IMethod> IType.Methods => Methods;

        IReadOnlyList<IProperty> IType.Properties => Properties;

        IReadOnlyList<IType> IType.NestedTypes => NestedTypes;

        public TypeReference(MC.TypeReference type) : this(type, Resolve(type.DeclaringType))
        {
            
        }

        internal TypeReference(MC.TypeReference type, TypeReference owner) : base(type.Name)
        {
            _ref = type;
            
            DeclaringType = owner;
        }

        internal static TypeReference Resolve(MC.TypeReference type)
        {
            if (type is null) return null;

            if (_cache.TryGetValue(type, out TypeReference result)) return result; 
            result = new(type);
            _cache.Add(type, result);
            return result;
        }
    }
}
