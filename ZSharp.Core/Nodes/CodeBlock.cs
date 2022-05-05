namespace ZSharp.Core
{
    public record class CodeBlock(string Source) : Node
    {
        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
