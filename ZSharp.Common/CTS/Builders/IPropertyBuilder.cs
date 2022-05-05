namespace ZSharp
{
    public interface IPropertyBuilder
        : IProperty
        , IBuilder
    {
        /// <summary>
        /// The getter for the property.
        /// </summary>
        new IMethodBuilder Getter { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        IMethod IProperty.Getter => Getter;

        /// <summary>
        /// The setter for the property.
        /// </summary>
        new IMethodBuilder Setter { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        IMethod IProperty.Setter => Setter;
    }
}