using System;

namespace ZSharp.OldCore
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class LanguageEngineAttribute : Attribute
    {
        public System.Type EngineType { get; }

        public LanguageEngineAttribute(System.Type type)
        {
            EngineType = type;
        }
    }
}
