namespace ZSharp.CG
{
    public class ParameterBuilder
        : NamedItem
        , IParameterBuilder
    {
        protected internal readonly ParameterDefinition _def;

        public IType Type { get; set; }

        public int Index => _def.Index;

        internal ParameterBuilder(string name, TypeReference type) : base(name)
        {
            _def = new(name, ParameterAttributes.None, type._ref);
            Type = type;
        }

        public void Build() { }
    }
}
