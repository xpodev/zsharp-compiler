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
    }
}
