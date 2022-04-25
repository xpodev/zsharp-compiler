namespace ZSharp.Core
{
    public record class CodeBlock(string Source) : Node
    {
        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
