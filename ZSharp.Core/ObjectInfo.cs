namespace ZSharp.Core
{
    public class ObjectInfo
    {
        public FileInfo FileInfo { get; }

        public Expression Expression { get; }

        public ObjectInfo(FileInfo fileInfo, Expression expression)
        {
            FileInfo = fileInfo;
            Expression = expression;
        }

        public static implicit operator Expression(ObjectInfo objectInfo) => objectInfo.Expression;

        public override bool Equals(object obj)
        {
            if (obj is FileInfo fileInfo) return Equals(fileInfo);
            return base.Equals(obj);
        }

        public bool Equals(ObjectInfo other) =>
            FileInfo.Equals(other.FileInfo) &&
            Expression.Equals(other.Expression);

        public override int GetHashCode()
        {
            return FileInfo.GetHashCode() ^ Expression.GetHashCode();
        }
    }
}
