namespace ZSharp.Core
{
    public class Keyword : Expression
    {
        public string Name { get; set; }

        public Keyword(string name)
        {
            Name = name;
        }

        public bool Equals(Keyword obj)
        {
            return Name == obj.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
