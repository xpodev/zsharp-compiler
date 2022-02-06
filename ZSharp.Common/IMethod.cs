namespace ZSharp
{
    /// <summary>
    /// Base definition of a method. A method is a function that's declared in a type.
    /// </summary>
    public interface IMethod
        : IFunction
    {
        /// <summary>
        /// The type that owns the method.
        /// </summary>
        IType DeclaringType { get; }


        /// <summary>
        /// Returns whether or not the method is declared as static.
        /// </summary>
        bool IsStatic { get; }
    }
}
