using System.Collections.Generic;
using ZSharp.CG;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class ProjectScope : ScopeBase
    {
        // temporary. should be private
        internal readonly Dictionary<string, DocumentScope> _documents = new();

        public DocumentScope Document => GetDocument(DocumentInfo); 

        public DocumentInfo DocumentInfo { get; private set; }

        public ModuleBuilder Module { get; } = new("test");

        public DocumentScope GetDocument(DocumentInfo document) => GetDocument(document.FileName);

        public DocumentScope GetDocument(string document) => _documents.GetValueOrDefault(document);

        internal void SetCurrentDocument(DocumentInfo info) => DocumentInfo = info;
    }
}
