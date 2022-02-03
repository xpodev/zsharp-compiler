using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public abstract class BaseProcessor : IExpressionProcessor
    {
        public BuildResult<ErrorType, Expression>? BuildContext { get; private set; }

        public Context Context { get; }

        public BaseProcessor(Context ctx)
        {
            if (ctx is null)
                throw new System.ArgumentNullException(nameof(ctx));

            Context = ctx;
        }

        #region Processing

        public virtual void PreProcess() { }

        public virtual List<BuildResult<ErrorType, ObjectInfo>> Process(List<BuildResult<ErrorType, ObjectInfo>> input) => input.Select(Process).ToList();

        public BuildResult<ErrorType, ObjectInfo> Process(BuildResult<ErrorType, ObjectInfo> input)
        {
            if (input.Value.Expression is null) return input;
            if (input.HasErrors) return input;

            BuildContext = input.Cast(o => o.Expression);

            BuildResult<ErrorType, ObjectInfo> result = Process(input.Value);

            BuildContext = null;

            return result;
        }

        public virtual BuildResult<ErrorType, ObjectInfo> Process(ObjectInfo info)
        {
            BuildResult<ErrorType, ObjectInfo> result = 
                BuildResultUtils.Bind<Expression>(info, Process)
                .Cast(info.With);

            return result;
        }

        public abstract BuildResult<ErrorType, Expression?> Process(Expression expr);

        public virtual void PostProcess() { }

        #endregion
    }
}
