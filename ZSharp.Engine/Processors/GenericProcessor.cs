using ZSharp.Core;

namespace ZSharp.Engine
{
    public class GenericProcessor<T> : BaseProcessor<string>
        where T : IGenericCompilable<T>
    {
        public ObjectInfo CurrentObject { get; private set; }

        public GenericProcessor(Context ctx) : base(ctx) { }

        public override Result<string, ObjectInfo> Process(ObjectInfo @object)
        {
            CurrentObject = @object;

            Result<string, ObjectInfo> result = Bind(@object, Process);

            CurrentObject = null;

            return new(@object);
        }

        public Result<string, Expression> Process(Expression expr)
        {
            return expr is T compilable ? new(compilable.Compile(this, Context), expr) : new(expr);
        }
    }
}
