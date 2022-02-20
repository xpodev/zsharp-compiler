using System.Collections.Generic;

namespace ZSharp.Core
{
    /// <summary>
    /// Interface for objects that can be modified.
    /// </summary>
    public interface IModifiable
    {
        /// <summary>
        /// Gets the list of modifiers.
        /// </summary>
        public IEnumerable<string> Modifiers { get; }

        /// <summary>
        /// Adds a modifier to the list of modifiers.
        /// </summary>
        public void AddModifier(string modifier);

        /// <summary>
        /// Checks if the list of modifiers contains a given modifier.
        /// </summary>
        public bool HasModifier(string modifier);
    }
}
