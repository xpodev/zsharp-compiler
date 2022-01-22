using ZSharp.Core;

namespace ZSharp.Engine
{
    public interface IGenericCompilable<T>
        where T : IGenericCompilable<T>
    {
        Expression Compile(GenericProcessor<T> proc, Context ctx);
    }
}
