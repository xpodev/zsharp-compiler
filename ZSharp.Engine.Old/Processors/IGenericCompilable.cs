using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public interface IGenericCompilable<T>
        where T : IGenericCompilable<T>
    {
        BuildResult<ErrorType, Expression?> Compile(GenericProcessor<T> proc, Context ctx);
    }
}
