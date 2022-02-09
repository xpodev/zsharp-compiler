using System.Collections.Generic;
using System.IO;

namespace ZSharp.Parser
{
    internal class Parser : IParser
    {
        private readonly DocumentParser _parser = new();

        public IEnumerable<ObjectInfo> Parse(TextReader input) => _parser.Parse(input);

        public void SetDocument(DocumentInfo document) => _parser.SetDoucument(document);
    }
}
