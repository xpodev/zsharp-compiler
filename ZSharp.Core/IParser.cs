using System.Collections.Generic;
using System.IO;

namespace ZSharp.Core
{
    public interface IParser
    {
        IEnumerable<ObjectInfo> Parse(TextReader input);

        public void SetDocument(DocumentInfo document);
    }
}
