namespace ZSharp.Language
{
    internal class FunctionBody : Node/*, IILCompilable*/
    {
        public Function DeclaringFunction { get; internal set; }

        public Node Code { get; internal set; } 
    }
}
