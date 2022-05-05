using System.Collections.Generic;
using System.Linq;

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
        /// The parameters this function has.
        /// </summary>
        IReadOnlyList<IParameter> Parameters { get; }

        /// <summary>
        /// Input parameters types.
        /// </summary>
        IReadOnlyList<IType> Input => Parameters.Select(p => p.Type).ToArray();

        /// <summary>
        /// Return type of the function.
        /// </summary>
        IType Output => ReturnParameter.Type;
    }
}
