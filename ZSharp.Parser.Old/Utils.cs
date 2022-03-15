using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using ZSharp.OldCore;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace ZSharp.Parser
{
    internal static class Utils
    {
        public static Parser<char, NodeInfo> CreateObjectInfo(DocumentInfo document, Parser<char, OldCore.Expression> parser) =>
            Map(
                (start, expr, end) => new NodeInfo(new(document, start.Line, start.Col, end.Line, end.Col), expr), 
                CurrentPos, 
                parser, 
                CurrentPos
                );
    }
}
