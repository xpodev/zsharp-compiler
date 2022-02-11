using System.Collections.Generic;

namespace ZSharp
{
    /// <summary>
    /// Base definition of a function.
    /// </summary>
    public interface IFunction
        : INamedItem
        , ITypedItem
    {
        /// <summary>
        /// The return value parameter.
        /// </summary>
        IParameter ReturnParameter { get; }

        /// <summary>
        /// Input parameters types.
        /// </summary>
        IEnumerable<IType> Input { get; }

        /// <summary>
        /// Return type of the function.
        /// </summary>
        IType Output => ReturnParameter.Type;
    }
}
