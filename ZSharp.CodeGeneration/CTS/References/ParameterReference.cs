namespace ZSharp.CG
{
    public class ParameterReference
        : NamedItem
        , IParameter
    {
        private readonly ParameterDefinition _ref;

        public FunctionReference DeclaringFunction { get; }

        public int Index => _ref.Index;

        public TypeReference Type { get; }

        IType ITypedItem.Type => Type;

        internal ParameterReference(ParameterDefinition def, FunctionReference owner)
            : base(def.Name)
        {
            _ref = def;
            DeclaringFunction = owner;
            Type = TypeReference.Resolve(def.ParameterType);
        }
    }
}
