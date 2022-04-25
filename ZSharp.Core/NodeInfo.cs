namespace ZSharp.Core
{
    public class NodeInfo
    {
        public FileInfo FileInfo { get; }

        public Node Object { get; }

        public NodeInfo(FileInfo fileInfo, Node node)
        {
            (Object = node).FileInfo = FileInfo = fileInfo;
        }

        public static implicit operator Node(NodeInfo objectInfo) => objectInfo.Object;

        public NodeInfo With(Node e) => new(FileInfo, e);

        public T Cast<T>() where T : Node => (T)Object;

        public override bool Equals(object obj)
        {
            if (obj is FileInfo fileInfo) return Equals(fileInfo);
            return base.Equals(obj);
        }

        public bool Equals(NodeInfo other) =>
            FileInfo.Equals(other.FileInfo) &&
            Object.Equals(other.Object);

        public override int GetHashCode() =>
            FileInfo.GetHashCode() ^ Object.GetHashCode();

        public override string ToString()
        {
            return Object.ToString();
        }
    }

    public class NodeInfo<T> : NodeInfo
        where T : Node
    {
        public new T Object { get; }

        public NodeInfo(FileInfo fileInfo, T @object) : base(fileInfo, @object)
        {
            Object = @object;
        }

        public NodeInfo<U> With<U>() where U : Node => new(FileInfo, Cast<U>());

        public NodeInfo<U> Select<U>(System.Func<T, U> func)
            where U : Node => 
            new(FileInfo, func(Object));
    }
}
