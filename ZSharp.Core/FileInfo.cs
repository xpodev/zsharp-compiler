using System;

namespace ZSharp.Core
{
    /// <summary>
    /// Contains span information in a certain document.
    /// </summary>
    public struct FileInfo
    {
        /// <summary>
        /// The document info.
        /// </summary>
        public DocumentInfo Document { get; }

        /// <summary>
        /// The line at which the span starts.
        /// </summary>
        public int StartLine { get; }

        /// <summary>
        /// The line at which the span ends.
        /// </summary>
        public int EndLine { get; }

        /// <summary>
        /// The column at which the span starts.
        /// </summary>
        public int StartColumn { get; }

        /// <summary>
        /// The column at which the span ends.
        /// </summary>
        public int EndColumn { get; }

        /// <summary>
        /// Creates a new FileInfo instance.
        /// </summary>
        /// <param name="document">The document that contains the span.</param>
        /// <param name="startLine">The line at which the span starts.</param>
        /// <param name="startColumn">The column at which the span starts.</param>
        /// <param name="endLine">The line at which the span ends.</param>
        /// <param name="endColumn">The column at which the span ends.</param>
        public FileInfo(DocumentInfo document, int startLine, int startColumn, int endLine, int endColumn)
        {
            Document = document;
            StartLine = startLine;
            StartColumn = startColumn;
            EndLine = endLine;
            EndColumn = endColumn;
        }

        /// <summary>
        /// Combines two FileInfo instances to include both spans. The order of parameters does not matter.
        /// </summary>
        /// <param name="a">The first span.</param>
        /// <param name="b">The second span.</param>
        /// <returns>A new FileInfo for a span that includes both span and everything in between them.</returns>
        /// <exception cref="ArgumentException">Thrown when the 2 spans are in different documents.</exception>
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

        /// <summary>
        /// Checks if two FileInfo instances are equal.
        /// </summary>
        /// <param name="other">The other file info.</param>
        /// <returns><see cref="true"/> if the 2 infos represent the same span in the same file, <see cref="false"/> otherwise.</returns>
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
