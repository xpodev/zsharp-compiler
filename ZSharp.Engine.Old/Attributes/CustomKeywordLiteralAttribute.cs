using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class CustomKeywordLiteralAttribute : CustomAttribute
    {
        public CustomKeywordLiteralAttribute(string keyword)
            : base(keyword)
        {

        }
    }
}
