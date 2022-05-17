namespace ZSharp.Language
{
    public record class FunctionTypeNode(NodeInfo<TypeNode> Input, NodeInfo<TypeNode> Output) : TypeNode
    {
        public override TypeNameNode AsTypeName()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Input} -> {Output}";
        }
    }
}
