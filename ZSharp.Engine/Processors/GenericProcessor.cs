using ZSharp.Core;

namespace ZSharp.Engine
{
    public class GenericProcessor<T> : BaseProcessor
        where T : IGenericCompilable<T>
    {
        public GenericProcessor(Context ctx) : base(ctx) { }

        public override ObjectInfo Process(ObjectInfo expr)
        {
            if (expr is T compilable) return compilable.Compile(this, Context);

            return base.Process(expr);
        }
    }
}
