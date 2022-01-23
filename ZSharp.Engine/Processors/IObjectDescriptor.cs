using ZSharp.Core;

namespace ZSharp.Engine
{
    public interface IObjectDescriptor
    {
        Expression Compile(ObjectDesciptorProcessor proc, Context ctx);
    }
}
