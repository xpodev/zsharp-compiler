using System.Reflection.Emit;

namespace ZSharp.Engine.Definitions
{
    /// <summary>
    /// Defined a compile time type definition
    /// </summary>
    public interface ISRFTypeDefinition
        : INamedItem
    {
        /// <summary>
        /// The compile-time definition (builder) of the type
        /// </summary>
        TypeBuilder Definition { get; }
    }
}
