namespace ZSharp.Language
{
    public record class FunctionType(NodeInfo<Type> Input, NodeInfo<Type> Output) : Type
    {
        public override TypeName AsTypeName()
        {
            throw new System.NotImplementedException();
        }

        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Input} -> {Output}";
        }
    }
}
