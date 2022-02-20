using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class CustomOperatorAttribute : CustomAttribute
    {
        public int Precedence { get; }

        public OldCore.OperatorFixity Fixity { get; }

        public OldCore.Associativity Associativity { get; }

        public CustomOperatorAttribute(string @operator, int precedence, OldCore.OperatorFixity fixity)
            : this(@operator, precedence, fixity, OldCore.Associativity.None)
        {

        }

        public CustomOperatorAttribute(
                string @operator, 
                int precedence, 
                OldCore.OperatorFixity fixity, 
                OldCore.Associativity associativity
            )
            : base(@operator)
        {
            Fixity = fixity;
            Precedence = precedence;
            Associativity = associativity;
        }
    }
}
