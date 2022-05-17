namespace ZSharp.Language
{
    public record class TypedItemNode(NodeInfo<Identifier> Name, NodeInfo<ModifiedObject<TypeNode>> Type) : Node
    {
        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Name}: {Type}";
        }
    }
}
