namespace ZSharp
{
    /// <summary>
    /// Base definition of a method. A method is a function that's declared in a type.
    /// </summary>
    public interface IMethod
        : IFunction
        , IMember
    {
        /// <summary>
        /// Returns whether or not the method is declared as static.
        /// </summary>
        bool IsStatic { get; }
    }
}
