using System.Collections.Generic;

namespace ZSharp
{
    /// <summary>
    /// Defines an item with generic parameters.
    /// </summary>
    public interface IGenericDefinition
    {
        IReadOnlyList<IGenericParameter> GenericParameters { get; }
    }
}
