namespace ZSharp
{
    /// <summary>
    /// Defines a type that builds a function and can be used as one.
    /// </summary>
    public interface IFunctionBuilder
        : IFunction
    {
        /// <summary>
        /// The return value parameter.
        /// </summary>
        new IParameterBuilder ReturnParameter { get; }

        /// <summary>
        /// Defines a parameter for the function. The parameter is placed last.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>A <see cref="IParameterBuilder"/> that builds the newly added parameter.</returns>
        IParameterBuilder DefineParameter(string name, IType type);
    }
}
