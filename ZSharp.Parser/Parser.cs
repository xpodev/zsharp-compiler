using System.Collections.Generic;
using System.IO;

namespace ZSharp.Parser
{
    public class Parser : IParser
    {
        private readonly DocumentParser _parser = new();

        public DocumentParser Document => _parser;

        public Parser()
        {
            
        }

        public void SetDocument(DocumentInfo document) => _parser.SetDocument(document);

        public IEnumerable<ObjectInfo> Parse(TextReader input) => _parser.Parse(input);

        public IEnumerable<ObjectInfo> ParseFile(string filePath)
        {
            using TextReader stream = File.OpenText(filePath);
            return Parse(stream);
        }

        public void Build() => _parser.Build(this);
    }
}
