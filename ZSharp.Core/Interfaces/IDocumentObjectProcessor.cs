using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface IDocumentObjectProcessor
    {
        void PreProcess();

        List<BuildResult<ErrorType, ObjectInfo>> Process(List<BuildResult<ErrorType, ObjectInfo>> expression);

        void PostProcess();
    }
}
