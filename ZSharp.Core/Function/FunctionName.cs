namespace ZSharp.Core
{
    public class FunctionName : Expression
    {
        public string Name { get; set; }

        public FunctionName(string name)
        {
            Name = name;
        }

        public bool Equals(FunctionName obj)
        {
            return Name == obj.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
