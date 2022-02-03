namespace ZSharp.Engine
{
    public class Parameter
        : NamedItem
        , IParameter
        , IParameterBuilder
    {
        public IType Type { get; }

        public short Position { get; }

        public IFunction DeclaringFunction { get; }

        public Mono.Cecil.ParameterDefinition MC => DeclaringFunction.MC.Parameters[Position];

        public Parameter(string name, IType type, int position, IFunction owner) : base(name)
        {
            Position = (short)position;
            Type = type;
            DeclaringFunction = owner;
        }
    }
}
