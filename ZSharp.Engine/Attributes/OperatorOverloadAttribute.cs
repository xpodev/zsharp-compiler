using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OperatorOverloadAttribute : Attribute
    {
        public string Operator { get; }

        public OperatorOverloadAttribute(string @operator)
        {
            Operator = @operator;
        }
    }
}
