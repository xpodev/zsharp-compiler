using System.Collections.Generic;

namespace ZSharp
{
    public interface IGenericParameter : INamedItem
    {
        /// <summary>
        /// Constains associated with this generic parameter.
        /// </summary>
        IReadOnlyList<IConstraint> Constraints { get; }

        /// <summary>
        /// The position of the parameter (0-based)
        /// </summary>
        int Index { get; }
    }
}