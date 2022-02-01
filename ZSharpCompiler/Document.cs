using System.Collections.Generic;
using ZSharp.Core;

namespace ZSharp.Compiler
{
    internal class Document
    {
        public DocumentInfo DocumentInfo { get; }

        public List<BuildResult<ErrorType, ObjectInfo>> Objects { get; set; }

        public Document(DocumentInfo documentInfo, List<BuildResult<ErrorType, ObjectInfo>> objects)
        {
            DocumentInfo = documentInfo;
            Objects = objects;
        }
    }
}
