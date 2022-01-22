using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class KeywordOverloadAttribute : Attribute
    {
        public string Keyword { get; }

        public KeywordOverloadAttribute(string keyword)
        {
            Keyword = keyword;
        }
    }
}
