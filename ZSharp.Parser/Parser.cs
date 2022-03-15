﻿using System.Collections.Generic;
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

        private void SetDocument(DocumentInfo document) => _parser.SetDocument(document);

        public IEnumerable<NodeInfo> Parse(TextReader input) => _parser.Parse(input);

        public IEnumerable<NodeInfo> ParseFile(string filePath)
        {
            using TextReader stream = File.OpenText(filePath);
            SetDocument(new(filePath));
            return Parse(stream);
        }

        public void Build() => _parser.Build(this);
    }
}
