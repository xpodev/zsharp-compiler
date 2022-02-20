namespace ZSharp.Engine
{
    public interface IParameter : INamedItem
    {
        short Position { get; }

        IFunction DeclaringFunction { get; }

        IType Type { get; }
    }
}
