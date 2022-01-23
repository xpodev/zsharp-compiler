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

            return new(
                a.Document,
                Math.Min(a.StartLine, b.StartLine),
                Math.Max(a.EndLine, b.EndLine),
                Math.Min(a.StartColumn, b.StartColumn),
                Math.Max(a.EndColumn, b.EndColumn)
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
    }
}
