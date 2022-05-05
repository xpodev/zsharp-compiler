using System.Collections.Generic;

namespace ZSharp
{
    /// <summary>
    /// Base definition for a type.
    /// </summary>
    public interface IType
        : IMember
    {
        IReadOnlyList<IField> Fields { get; }

        IReadOnlyList<IMethod> Methods { get; }

        IReadOnlyList<IProperty> Properties { get; }

        IReadOnlyList<IType> NestedTypes { get; }

        IReadOnlyList<IType> Interfaces { get; }
    }
}
