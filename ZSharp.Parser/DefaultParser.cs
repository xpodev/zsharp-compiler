using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
using ZSharp.Core;

namespace ZSharp.Parser
{
    public partial class DefaultParser : IParser
    {
        internal partial class ExpressionParser
        {

        }

        //private Parser<char, ObjectInfo> _parser = Expression.Single;

        private DocumentInfo _document;

        public IEnumerable<ObjectInfo> Parse(TextReader stream) => Expression.Single.Many().ParseOrThrow(stream);

        public void SetDocument(DocumentInfo document) => ParserState.Reset(_document = document);

        public Parser<char, ObjectInfo> CreateFileInfo(Parser<char, Core.Expression> parser) => Utils.CreateObjectInfo(_document, parser);
    }
}
