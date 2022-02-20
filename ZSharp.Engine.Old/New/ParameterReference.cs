namespace ZSharp.Engine
{
    public class ParameterReference
        : NamedItem
        , IParameter
    {
        public short Position { get; }

        public IFunction DeclaringFunction { get; }

        public IType Type { get; }

        public ParameterReference(
            IFunction owner, 
            IType type,
            int position, 
            string name = null
            ) 
            : base(name)
        {
            DeclaringFunction = owner;
            Position = (short)position;
            Type = type;
        }
    }
}
