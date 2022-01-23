using ZSharp.Core;

namespace ZSharp.Engine
{
    public interface IInvocable
        : INamedItem
    {
        Result<string, Expression> Invoke(params object[] args);
    }
}
