namespace ZSharp.Core
{
    public record class Keyword(string Name) : Expression
    {
        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
