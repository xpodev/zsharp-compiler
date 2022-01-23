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
    }
}
