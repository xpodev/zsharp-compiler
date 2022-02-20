using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public interface IDependencyFinder
    {
        BuildResult<ErrorType, Expression?> Compile(DependencyFinder finder, Context context);
    }
}
