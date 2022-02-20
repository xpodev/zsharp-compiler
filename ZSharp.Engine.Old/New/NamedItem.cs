using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public abstract class NamedItem 
        : Expression
        , INamedItem
    {
        public string Name { get; }

        public NamedItem(string name)
        {
            if (name is null)
                throw new System.ArgumentNullException(nameof(name));
            Name = name;
        }

        public override string ToString()
        {
            return GetType().Name + " " + Name;
        }
    }
}
