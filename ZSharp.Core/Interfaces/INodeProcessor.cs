using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface INodeProcessor
    {
        void PreProcess();

        List<ObjectBuildResult> Process(List<ObjectBuildResult> expression);

        void PostProcess();
    }
}
