namespace ZSharp.Core
{
    /// <summary>
    /// Represents a node in a document.
    /// </summary>
    public abstract record class Node
    {
        /// <summary>
        /// File info for this node.
        /// </summary>
        public FileInfo FileInfo { get; internal set; }

        /// <summary>
        /// Constructs and returns the object this node represents.
        /// </summary>
        /// <returns>A compiler object this node represents.</returns>
        public abstract Object GetCompilerObject();
    }
}
