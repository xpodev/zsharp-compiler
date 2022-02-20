namespace ZSharp.SR
{
    public class FieldBuilder
        : NamedItem
        , IFieldBuilder
    {
        protected internal RE.FieldBuilder _def;

        public TypeBuilder DeclaringType { get; }

        public TypeReference Type { get; }

        IType IMember.DeclaringType => DeclaringType;

        IType ITypedItem.Type => Type;

        public FieldBuilder(string name, TypeBuilder owner, TypeReference type) : base(name)
        {
            _def = owner._def.DefineField(name, type._ref, FieldAttributes.PrivateScope);
            Type = type;
        }

        public void Build()
        {

        }
    }
}
