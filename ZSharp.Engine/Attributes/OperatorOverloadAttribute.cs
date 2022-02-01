using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OperatorOverloadAttribute : Attribute
    {
        public string Operator { get; }

        public bool IsPrefix { get; }

        public OperatorOverloadAttribute(string @operator, bool isPrefix = true)
        {
            Operator = @operator;
            IsPrefix = isPrefix;
        }
    }
}
