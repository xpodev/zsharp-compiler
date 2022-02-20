using System;

namespace ZSharp.Core
{
    public struct FileInfo
    {
        public DocumentInfo Document { get; }

        public int StartLine { get; }

        public int EndLine { get; }

        public int StartColumn { get; }

        public int EndColumn { get; }

        public FileInfo(DocumentInfo document, int startLine, int startColumn, int endLine, int endColumn)
        {
            Document = document;
            StartLine = startLine;
            StartColumn = startColumn;
            EndLine = endLine;
            EndColumn = endColumn;
        }

        public static FileInfo Combine(FileInfo a, FileInfo b)
        {
            if (a.Document != b.Document)
                throw new ArgumentException($"Can't combine infos of different documents \'{a.Document}\' and \'{b.Document}\'");

            FileInfo start, end;
            if (a.StartLine != b.StartLine)
                start = a.StartLine < b.StartLine ? ref a : ref b;
            else
                start = a.StartColumn < b.StartColumn ? ref a : ref b;

            if (a.EndLine != b.EndLine)
                end = a.EndLine > b.EndLine ? ref a : ref b;
            else
                end = a.EndColumn > b.EndColumn ? ref a : ref b;

            return new(
                a.Document,
                start.StartLine,
                start.StartColumn,
                end.EndLine,
                end.EndColumn
                );
        }

        public override bool Equals(object obj)
        {
            if (obj is FileInfo fileInfo) return Equals(ref fileInfo);
            return base.Equals(obj);
        }

        public bool Equals(ref FileInfo other) =>
            Document == other.Document &&
            StartLine == other.StartLine &&
            EndLine == other.EndLine &&
            StartColumn == other.StartColumn &&
            EndColumn == other.EndColumn;

        public override int GetHashCode() =>
            Document.GetHashCode() ^
            StartLine.GetHashCode() ^
            EndLine.GetHashCode() ^
            StartColumn.GetHashCode() ^
            EndColumn.GetHashCode();

        public static bool operator ==(FileInfo left, FileInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FileInfo left, FileInfo right)
        {
            return !(left == right);
        }
    }
}
