using System;

namespace ZSharp.Engine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class CustomSurroundingOperatorAttribute : CustomAttribute
    {
        public string Left { get; }

        public string Right { get; }

        public int Precedence { get; }

        public bool IsPrefix { get; }

        public bool AllowMultiple { get; }

        public CustomSurroundingOperatorAttribute(
            string left, string right,
            int precedence, bool isPrefix, 
            bool allowMultiple = false)
            : base($"{left}operator{right}")
        {
            Left = left;
            Right = right;
            Precedence = precedence;
            IsPrefix = isPrefix;
            AllowMultiple = allowMultiple;
        }
    }
}
