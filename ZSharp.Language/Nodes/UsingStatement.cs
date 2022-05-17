using ZSharp.Engine;

namespace ZSharp.Language
{
    internal abstract record class UsingStatement : Node, IContextPreparationItem
    {
        public abstract BuildResult<Error, Node> Process(Processor<IContextPreparationItem> proc);

        public override Object GetCompilerObject(IContext ctx) => null;
    }
}
