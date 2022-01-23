using ZSharp.Core;

namespace ZSharp.Engine
{
    public class ObjectDesciptorProcessor : BaseProcessor<string>
    {
        public ObjectDesciptorProcessor(Context ctx) : base(ctx)
        {
        }

        public override Result<string, ObjectInfo> Process(ObjectInfo @object)
        {
            return Bind(@object, Process);
        }

        public Result<string, Expression> Process(Expression expression)
        {
            if (expression is IObjectDescriptor descriptor)
                return new(descriptor.Compile(this, Context));

            return new(expression);
        }
    }
}
