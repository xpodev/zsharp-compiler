namespace ZSharp.CG
{
    public class ReturnParameterBuilder
        : NamedItem
        , IParameterBuilder
    {
        private TypeReference _type;

        protected internal readonly MethodReturnType _def;

        public int Index => -1;

        public TypeReference Type
        {
            get => _type;
            set
            {
                _type = value;
                _def.ReturnType = value._ref;
            }
        }

        IType ITypedItem.Type => Type;

        internal ReturnParameterBuilder(MethodReturnType def, TypeReference type) : base(def.Name)
        {
            _def = def;
            _type = type;
        }
    }
}
