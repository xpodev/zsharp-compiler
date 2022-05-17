namespace ZSharp.Language
{
    public record class TypedItemNode(NodeInfo<Identifier> Name, NodeInfo<TypeNode> Type) : Node
    {
        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
