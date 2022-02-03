namespace ZSharp.Core
{
    public class FunctionCall : Expression
    {
        public ObjectInfo Callable { get; set; }

        public ObjectInfo Argument { get; set; }

        public bool IsPrefix { get; set; }

        public string Name { get; set; }

        public FunctionCall(ObjectInfo callable, ObjectInfo argument)
        {
            Callable = callable;
            Argument = argument;
        }
    }
}
