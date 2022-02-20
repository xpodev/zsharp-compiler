using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public class ObjectDesciptorProcessor : BaseProcessor
    {
        public ObjectDesciptorProcessor(Context ctx) : base(ctx)
        {
        }

        public override BuildResult<ErrorType, Expression> Process(Expression expression)
        {
            if (expression is IObjectDescriptor descriptor)
                return new(descriptor.Compile(this, Context));

            return new(expression);
        }
    }
}
