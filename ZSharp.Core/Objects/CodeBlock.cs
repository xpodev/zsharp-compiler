namespace ZSharp.Core
{
    public class CodeBlock : Node
    {
        public string Source { get; }

        public CodeBlock(string source)
        {
            Source = source;
        }
    }
}
