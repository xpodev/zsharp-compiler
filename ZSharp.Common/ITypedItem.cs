namespace ZSharp
{
    /// <summary>
    /// Defines an item that has a type.
    /// </summary>
    public interface ITypedItem
    {
        /// <summary>
        /// The type of the item.
        /// </summary>
        IType Type { get; }
    }
}
