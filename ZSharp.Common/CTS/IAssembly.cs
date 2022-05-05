using System.Collections.Generic;

namespace ZSharp
{
    public interface IAssembly : INamedItem
    {
        IEnumerable<IType> GetDefinedTypes();

        IEnumerable<IType> GetForwardedTypes();
    }
}
