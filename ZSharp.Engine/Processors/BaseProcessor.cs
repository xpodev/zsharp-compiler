using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public abstract class BaseProcessor : INodeProcessor
    {
        public Engine Engine { get; }

        public BaseProcessor(Engine engine)
        {
            Engine = engine;
        }

        public virtual void PostProcess() { }

        public virtual void PreProcess() { }

        public virtual List<BuildResult<ErrorType, NodeInfo>> Process(List<BuildResult<Error, NodeInfo>> nodes)
            => nodes.Select(Process).ToList();

        public BuildResult<ErrorType, NodeInfo> Process(BuildResult<ErrorType, NodeInfo> input)
        {
            if (input.Value.Object is null) return input;
            if (input.HasErrors) return input;

            return Process(input.Value);
        }

        public virtual BuildResult<ErrorType, NodeInfo> Process(NodeInfo info)
        {
            Engine.Context.SetCurrentDocument(info.FileInfo.Document);

            BuildResult<ErrorType, NodeInfo> result =
                BuildResultUtils.Bind<Node>(info, Process)
                .Cast(info.With);

            return result;
        }

        public abstract BuildResult<ErrorType, Node> Process(Node node);
    }
}
