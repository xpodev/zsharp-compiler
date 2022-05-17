namespace ZSharp.Language
{
    public record class FunctionTypeNode(NodeInfo<ModifiedObject<TypeNode>> Input, NodeInfo<ModifiedObject<TypeNode>> Output) : TypeNode
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
