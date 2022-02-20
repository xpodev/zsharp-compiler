namespace ZSharp.Language
{
    internal class FunctionBody : DocumentObject/*, IILCompilable*/
    {
        public Function DeclaringFunction { get; internal set; }

        public DocumentObject Code { get; internal set; } 
    }
}
