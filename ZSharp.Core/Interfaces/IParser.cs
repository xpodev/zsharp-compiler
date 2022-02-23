using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface IParser
    {
        IEnumerable<ObjectInfo> ParseFile(string filePath);
    }
}
