namespace ZSharp
{
    /// <summary>
    /// Defines a parameter of a function.
    /// </summary>
    public interface IParameter
        : INamedItem
        , ITypedItem
    {
        /// <summary>
        /// The position of the parameter (0-based)
        /// </summary>
        int Index { get; }
    }
}
