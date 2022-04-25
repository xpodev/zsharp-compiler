using ZSharp.Core;

namespace ZSharp.Engine
{
    internal interface IInvocable
    {
        BuildResult<ErrorType, object> Invoke(params object[] args);
    }
}
