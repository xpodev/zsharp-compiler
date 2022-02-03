namespace ZSharp.Core
{
    public class DocumentInfo
    {
        public string Path { get; }

        public string FileName => System.IO.Path.GetFileName(Path);

        public string Extension => System.IO.Path.GetExtension(Path);

        public DocumentInfo(string path)
        {
            Path = path;
        }

        public override string ToString()
        {
            return Path;
        }

        public override bool Equals(object obj)
        {
            if (obj is DocumentInfo document) return Equals(document);
            return base.Equals(obj);
        }

        public bool Equals(DocumentInfo other) => Path == other.Path;

        public override int GetHashCode() => Path.GetHashCode();
    }
}
