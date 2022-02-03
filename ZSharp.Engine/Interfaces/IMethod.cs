namespace ZSharp.Engine
{
    public interface IMethod 
        : IFunction
    {
        IType DeclaringType { get; }
    }
}
