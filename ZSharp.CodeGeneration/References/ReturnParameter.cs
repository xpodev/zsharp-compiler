namespace ZSharp.CG
{
    public class ReturnParameter
        : NamedItem
        , IParameter
    {
        protected internal readonly MethodReturnType _def;

        public int Index => -1;

        public TypeReference Type { get; }

        IType ITypedItem.Type => Type;

        internal ReturnParameter(MethodReturnType def) : base(def.Name)
        {
            _def = def;
            Type = TypeReference.Resolve(def.ReturnType);
        }
    }
}
