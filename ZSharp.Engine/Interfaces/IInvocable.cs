using ZSharp.Core;

namespace ZSharp.Engine
{
    public interface IInvocable
        : INamedItem
    {
        BuildResult<ErrorType, Expression?> Invoke(params Expression[] args);
    }
}
