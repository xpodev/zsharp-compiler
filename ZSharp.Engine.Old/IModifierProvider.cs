using System.Collections.Generic;

namespace ZSharp.Engine
{
    public interface IModifierProvider<T>
    {
        public void AddModifier(T modifier);

        public bool HasModifier(T modifier);
    }
}
