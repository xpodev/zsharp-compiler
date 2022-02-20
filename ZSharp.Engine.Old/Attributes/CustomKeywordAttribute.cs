using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class CustomKeywordAttribute : CustomAttribute
    {
        public int Precedence { get; }

        public OldCore.OperatorFixity Fixity { get; }

        public CustomKeywordAttribute(string kw, int precedence, OldCore.OperatorFixity fixity)
            : base(kw)
        {
            Precedence = precedence;
            Fixity = fixity;
        }
    }
}
