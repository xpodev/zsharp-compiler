namespace ZSharp
{
    public class NamedItem
        : INamedItem
    {
        public string Name { get; }

        public NamedItem(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{GetType().Name} {Name}";
        }
    }
}
