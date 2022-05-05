namespace ZSharp.CG
{
    public class FieldReference
        : NamedItem
        , IField
    {
        internal protected readonly MC.FieldReference _ref;

        public TypeReference Type { get; }

        public TypeReference DeclaringType { get; }

        IType ITypedItem.Type => Type;

        IType IMember.DeclaringType => DeclaringType;

        public FieldReference(MC.FieldReference field) : base(field.Name)
        {
            _ref = field;

            Type = TypeReference.Resolve(field.FieldType);
            DeclaringType = TypeReference.Resolve(field.DeclaringType);
        }
    }
}
