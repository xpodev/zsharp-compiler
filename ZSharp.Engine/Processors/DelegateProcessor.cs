using ZSharp.Core;

namespace ZSharp.Engine
{
    public class DelegateProcessor<T> 
        : BaseProcessor
        where T : IDelegatedProcessor<T>
    {
        public DelegateProcessor(Engine engine) : base(engine) { }

        public override BuildResult<ErrorType, Node> Process(Node node)
        {
            return node is T item ? item.Process(this) : new(node);
        }
    }
}
