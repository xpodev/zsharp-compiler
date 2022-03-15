namespace ZSharp.Core
{
    public class FunctionCall : Expression
    {
        public NodeInfo Callable { get; set; }

        public NodeInfo Argument { get; set; }

        public bool IsPrefix { get; set; }

        public string Name { get; set; }

        public FunctionCall(NodeInfo callable, NodeInfo argument)
        {
            Callable = callable;
            Argument = argument;
        }
    }
}
