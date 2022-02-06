namespace ZSharp
{
    /// <summary>
    /// Defines a type that builds a type and can be used as one.
    /// </summary>
    public interface ITypeBuilder
        : IType
    {
        /// <summary>
        /// Define a nested type and return its builder.
        /// </summary>
        /// <param name="name">The name of the nested type.</param>
        /// <returns>A <see cref="ITypeBuilder"/> that builds the newly added nested type.</returns>
        ITypeBuilder DefineType(string name);

        /// <summary>
        /// Define a method inside the type and return its builder.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <returns>A <see cref="IMethodBuilder"/> that builds the newly added method.</returns>
        IMethodBuilder DefineMethod(string name);
    }
}
