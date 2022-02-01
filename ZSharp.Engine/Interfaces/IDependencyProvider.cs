using System.Collections.Generic;

namespace ZSharp.Engine
{
    public interface IDependencyProvider<T>
    {
        IEnumerable<T> GetDependencies(T dependant);
    }
}
