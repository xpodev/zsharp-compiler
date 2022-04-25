namespace ZSharp.Core
{
    /// <summary>
    /// Base class for all compile time objects in a document.
    /// </summary>
    public abstract class Object
    {
        /// <summary>
        /// The node which represents this object.
        /// </summary>
        public Node Node { get; }

        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="node">The node which represents this object.</param>
        public Object(Node node)
        {
            Node = node;
        }
    }
}
