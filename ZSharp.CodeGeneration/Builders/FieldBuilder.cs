namespace ZSharp.CG
{
    public class FieldBuilder
        : NamedItem
        , IFieldBuilder
    {
        private TypeReference _type;

        protected internal readonly FieldDefinition _def;

        public TypeBuilder DeclaringType { get; }

        IType IMember.DeclaringType => DeclaringType;

        public TypeReference Type
        {
            get => _type;
            set
            {
                _type = value;
                _def.FieldType = value._ref;
            }
        }

        IType ITypedItem.Type => Type;

        public FieldBuilder(string name, TypeBuilder owner, TypeReference type) : base(name)
        {
            _def = new(name, FieldAttributes.CompilerControlled, type._ref);
            DeclaringType = owner;
            _type = type;
        }
    }
}
