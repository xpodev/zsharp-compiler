namespace ZSharp.Engine
{
    internal class CodeGenerator : DelegateProcessor<ICompilable>
    {
        public CodeGenerator(Engine engine) : base(engine)
        {
        }
    }
}
