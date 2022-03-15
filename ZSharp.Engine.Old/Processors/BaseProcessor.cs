using System.Collections.Generic;
using System.Linq;
using ZSharp.OldCore;

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

        public virtual List<BuildResult<ErrorType, NodeInfo>> Process(List<BuildResult<ErrorType, NodeInfo>> input) => input.Select(Process).ToList();

        public BuildResult<ErrorType, NodeInfo> Process(BuildResult<ErrorType, NodeInfo> input)
        {
            if (input.Value.Expression is null) return input;
            if (input.HasErrors) return input;

            BuildContext = input.Cast(o => o.Expression);

            BuildResult<ErrorType, NodeInfo> result = Process(input.Value);

            BuildContext = null;

            return result;
        }

        public virtual BuildResult<ErrorType, NodeInfo> Process(NodeInfo info)
        {
            BuildResult<ErrorType, NodeInfo> result = 
                BuildResultUtils.Bind<Expression>(info, Process)
                .Cast(info.With);

            return result;
        }

        public abstract BuildResult<ErrorType, Expression?> Process(Expression expr);

        public virtual void PostProcess() { }

        #endregion
    }
}
