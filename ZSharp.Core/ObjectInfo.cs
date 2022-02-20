namespace ZSharp.Core
{
    public class ObjectInfo
    {
        public FileInfo FileInfo { get; }

        public DocumentObject Object { get; }

        public ObjectInfo(FileInfo fileInfo, DocumentObject @object)
        {
            FileInfo = fileInfo;
            Object = @object;
        }

        public static implicit operator DocumentObject(ObjectInfo objectInfo) => objectInfo.Object;

        public ObjectInfo With(DocumentObject e) => new(FileInfo, e);

        public T Cast<T>() where T : DocumentObject => (T)Object;

        public override bool Equals(object obj)
        {
            if (obj is FileInfo fileInfo) return Equals(fileInfo);
            return base.Equals(obj);
        }

        public bool Equals(ObjectInfo other) =>
            FileInfo.Equals(other.FileInfo) &&
            Object.Equals(other.Object);

        public override int GetHashCode() =>
            FileInfo.GetHashCode() ^ Object.GetHashCode();

        public override string ToString()
        {
            return Object.ToString();
        }
    }

    public class ObjectInfo<T> : ObjectInfo
        where T : DocumentObject
    {
        public new T Object { get; }

        public ObjectInfo(FileInfo fileInfo, T @object) : base(fileInfo, @object)
        {
            Object = @object;
        }

        public ObjectInfo<U> With<U>() where U : DocumentObject => new(FileInfo, Cast<U>());

        public ObjectInfo<U> Select<U>(System.Func<T, U> func)
            where U : DocumentObject => 
            new(FileInfo, func(Object));
    }
}
