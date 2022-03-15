using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface IParser
    {
        IEnumerable<NodeInfo> ParseFile(string filePath);
    }
}
