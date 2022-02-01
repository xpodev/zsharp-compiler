using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class KeywordOverloadAttribute : Attribute
    {
        public string Keyword { get; }

        public bool IsPrefix { get; }

        public KeywordOverloadAttribute(string keyword, bool isPrefix = true)
        {
            Keyword = keyword;
            IsPrefix = isPrefix;
        }
    }
}
