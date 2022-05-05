namespace ZSharp.Language
{
    public abstract record class Type : Node
    {
        public static AutoType Infer { get; } = new();

        public static UnitType Unit { get; } = new();

        public abstract TypeName AsTypeName();

        public override Object GetCompilerObject(IContext ctx)
        {
            return AsTypeName().GetCompilerObject(TODO);
        }
    }
}
