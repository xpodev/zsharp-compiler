namespace ZSharp.Core
{
    public abstract class NodeWrapper<T> : Object
        where T : Node
    {
        public new T Node { get; }

        public NodeWrapper(T node)
            : base(node)
        {
            Node = node;
        }

        public override string ToString()
        {
            return Node.ToString();
        }
    }
}
