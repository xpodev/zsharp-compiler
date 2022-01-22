namespace ZSharp.Core
{
    public class Identifier : Expression
    {
        public string Name { get; set; }

        public Identifier(string name)
        {
            Name = name;
        }
    }
}
