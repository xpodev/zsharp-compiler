namespace ZSharp
{
    /// <summary>
    /// Defines a constraint for a generic parameter.
    /// </summary>
    public interface IConstraint
    {
        /// <summary>
        /// The definition of the constraint.
        /// </summary>
        IType Constraint { get; }
    }
}
