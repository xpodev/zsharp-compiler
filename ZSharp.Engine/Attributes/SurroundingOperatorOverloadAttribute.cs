namespace ZSharp.Engine
{
    public class SurroundingOperatorOverloadAttribute : OperatorOverloadAttribute
    {
        public bool IsPrefix { get; }

        public SurroundingOperatorOverloadAttribute(string left, string right, bool isPrefix = false)
            : base($"{left}_{right}")
        {
            IsPrefix = isPrefix;
        }
    }
}
