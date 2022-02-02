using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using ZSharp.Core;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace ZSharp.Parser
{
    internal static class Utils
    {
        public static Parser<char, ObjectInfo> CreateObjectInfo(DocumentInfo document, Parser<char, Core.Expression> parser) =>
            Map(
                (start, expr, end) => new ObjectInfo(new(document, start.Line, start.Col, end.Line, end.Col), expr), 
                CurrentPos, 
                parser, 
                CurrentPos
                );
    }
}
