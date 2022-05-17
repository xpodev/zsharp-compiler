namespace ZSharp.Language
{
    public abstract record class TypeNode : Node
    {
        public static AutoTypeNode Infer { get; } = new();

        public static UnitTypeNode Unit { get; } = new UnitTypeNode();

        public abstract TypeNameNode AsTypeName();

        public override Type GetCompilerObject(IContext ctx)
        {
            return AsTypeName().GetCompilerObject(ctx);
        }
    }
}
