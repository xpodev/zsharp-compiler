using ZSharp.Core;

namespace ZSharp.Engine
{
    public class GenericProcessor<T> : BaseProcessor
        where T : IGenericCompilable<T>
    {
        public GenericProcessor(Context ctx) : base(ctx) { }

        public override BuildResult<ErrorType, Expression?> Process(Expression expr)
        {
            return expr is T compilable ? compilable.Compile(this, Context) : new(expr);
        }
    }
}
