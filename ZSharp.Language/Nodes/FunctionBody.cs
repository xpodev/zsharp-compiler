namespace ZSharp.Language
{
    internal record class FunctionBody : Node/*, IILCompilable*/
    {
        public Function DeclaringFunction { get; internal set; }

        public Node Code { get; internal set; }

        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
