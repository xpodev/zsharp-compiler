namespace ZSharp.Language
{
    public record class AutoTypeNode : TypeNode
    {
        public override Type GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }

        public override TypeNameNode AsTypeName()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "auto";
        }
    }
}
