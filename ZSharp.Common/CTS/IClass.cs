namespace ZSharp
{
    /// <summary>
    /// Defines a class.
    /// </summary>
    public interface IClass : IType
    {
        /// <summary>
        /// Gets the base class.
        /// </summary>
        IClass Base { get; }
    }
}
