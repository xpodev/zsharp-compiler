namespace ZSharp.Language
{
    public record class FunctionBodyNode : Node/*, IILCompilable*/
    {
        public FunctionNode DeclaringFunction { get; internal set; }

        public Node Code { get; internal set; }

        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
