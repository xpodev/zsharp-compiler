namespace ZSharp.Core
{
    public struct Error
    {
        public string Message { get; }

        public NodeInfo Object { get; }

        public FileInfo FileInfo => Object.FileInfo;

        public Error(string message) : this(message, null) { }

        public Error(string message, NodeInfo info)
        {
            Message = message;
            Object = info;
        }

        public Error With(NodeInfo info) => new(Message, info);

        public override string ToString()
        {
            return $"({FileInfo.StartLine}, {FileInfo.StartColumn}): {Message}";
        }
    }
}
