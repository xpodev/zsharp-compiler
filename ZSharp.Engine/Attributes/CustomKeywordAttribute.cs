using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class CustomKeywordAttribute : CustomAttribute
    {
        public int Precedence { get; }

        public Core.OperatorFixity Fixity { get; }

        public CustomKeywordAttribute(string kw, int precedence, Core.OperatorFixity fixity)
            : base(kw)
        {
            Precedence = precedence;
            Fixity = fixity;
        }
    }
}
