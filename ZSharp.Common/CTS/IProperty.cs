namespace ZSharp
{
    /// <summary>
    /// Defines a property in a type.
    /// </summary>
    public interface IProperty 
        : IMember
        , ITypedItem
    {
        /// <summary>
        /// The getter for the property.
        /// </summary>
        IMethod Getter { get; }

        /// <summary>
        /// The setter for the property.
        /// </summary>
        IMethod Setter { get; }

        /// <summary>
        /// Returns whether the property has a get accessor.
        /// </summary>
        bool HasGetter { get => Getter != null; }

        /// <summary>
        /// Returns whether the property has a set accessor.
        /// </summary>
        bool HasSetter { get => Setter != null; }
    }
}
