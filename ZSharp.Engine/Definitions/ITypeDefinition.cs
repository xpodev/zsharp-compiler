namespace ZSharp.Engine.Definitions
{
    /// <summary>
    /// Describes a type that is being built by the compiler
    /// </summary>
    public interface ITypeDefinition 
        : INamedItem
        , IRuntimeTypeDefinition
        , ISRFTypeDefinition
        , IType
    {
        /// <summary>
        /// The type this type derives from
        /// </summary>
        IType BaseType { get; }

        ITypeDefinition DeclaringType { get; }
    }
}
