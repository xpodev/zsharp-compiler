namespace ZSharp.Core
{
    public record class FunctionCall(NodeInfo Callable, NodeInfo Argument) : Expression
    {
        public bool IsPrefix { get; set; }

        public string Name { get; set; }

        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
