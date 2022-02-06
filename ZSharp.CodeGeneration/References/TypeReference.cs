namespace ZSharp.CG
{
    public class TypeReference
        : NamedItem
        , IType
    {
        private static readonly System.Collections.Generic.Dictionary<MC.TypeReference, TypeReference> _cache = new();

        protected internal readonly MC.TypeReference _ref;

        public TypeReference DeclaringType { get; }

        IType IMember.DeclaringType => DeclaringType;

        public TypeReference(MC.TypeReference type, TypeReference owner) : base(type.Name)
        {
            _ref = type;
            DeclaringType = owner;
        }

        internal static TypeReference Resolve(MC.TypeReference type)
        {
            if (type is null) return null;

            if (_cache.TryGetValue(type, out TypeReference result)) return result; 
            result = new(type, Resolve(type.DeclaringType));
            _cache.Add(type, result);
            return result;
        }
    }
}
