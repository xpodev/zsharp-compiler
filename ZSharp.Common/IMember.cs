namespace ZSharp
{
    /// <summary>
    /// Defines an object who's defined inside a type definition.
    /// </summary>
    public interface IMember
        : INamedItem
    {
        /// <summary>
        /// The owning type.
        /// </summary>
        IType DeclaringType { get; }
    }
}
