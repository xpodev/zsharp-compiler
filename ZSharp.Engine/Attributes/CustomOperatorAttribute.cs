using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class CustomOperatorAttribute : CustomAttribute
    {
        public int Precedence { get; }

        public Core.OperatorFixity Fixity { get; }

        public Core.Associativity Associativity { get; }

        public CustomOperatorAttribute(string @operator, int precedence, Core.OperatorFixity fixity)
            : this(@operator, precedence, fixity, Core.Associativity.None)
        {

        }

        public CustomOperatorAttribute(
                string @operator, 
                int precedence, 
                Core.OperatorFixity fixity, 
                Core.Associativity associativity
            )
            : base(@operator)
        {
            Fixity = fixity;
            Precedence = precedence;
            Associativity = associativity;
        }
    }
}
