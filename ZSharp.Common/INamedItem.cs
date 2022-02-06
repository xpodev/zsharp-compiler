namespace ZSharp
{
    /// <summary>
    /// Defines an item that can be referred to by its name.
    /// </summary>
    public interface INamedItem
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        string Name { get; }
    }
}
