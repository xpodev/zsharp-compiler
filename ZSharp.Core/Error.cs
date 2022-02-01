namespace ZSharp.Core
{
    public struct Error
    {
        public string Message { get; }

        public ObjectInfo Object { get; }

        public FileInfo FileInfo => Object.FileInfo;

        public Error(string message) : this(message, null) { }

        public Error(string message, ObjectInfo info)
        {
            Message = message;
            Object = info;
        }

        public Error With(ObjectInfo info) => new(Message, info);

        public override string ToString()
        {
            return $"({FileInfo.StartLine}, {FileInfo.StartColumn}): {Message}";
        }
    }
}
