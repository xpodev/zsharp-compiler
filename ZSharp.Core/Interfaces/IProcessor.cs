using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface IProcessor<T>
        where T : class
    {
        void PreProcess();

        List<BuildResult<ErrorType, T>> Process(List<BuildResult<ErrorType, T>> items);

        void PostProcess();
    }
}
