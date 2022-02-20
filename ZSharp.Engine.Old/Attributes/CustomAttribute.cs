using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class CustomAttribute : Attribute
    {
        public string Name { get; }

        public CustomAttribute(string name)
        {
            Name = name;
        }
    }
}
