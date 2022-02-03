using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface IExpressionProcessor
    {
        void PreProcess();

        List<BuildResult<ErrorType, ObjectInfo>> Process(List<BuildResult<ErrorType, ObjectInfo>> expression);

        void PostProcess();
    }
}
