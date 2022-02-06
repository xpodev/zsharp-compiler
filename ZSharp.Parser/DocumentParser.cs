using Pidgin;
using Pidgin.Comment;
using System.Collections.Generic;
using System.IO;
using static Pidgin.Parser;

namespace ZSharp.Parser
{
    internal class DocumentParser
    {
        private Core.DocumentInfo _document;
        private readonly Parser<char, IEnumerable<Core.ObjectInfo>> _documentParser;

        private readonly Parser<char, Unit> _anyWhitespace;

        internal TermParser Term { get; }

        internal DocumentParser()
        {
            Parser<char, Unit> comments, whitespaces;

            comments = 
                Try(CommentParser.SkipLineComment(String("//")))
                .Or(CommentParser.SkipBlockComment(String("/*"), String("*/")))
                .SkipMany();

            whitespaces = Whitespaces.IgnoreResult().Or(EndOfLine.IgnoreResult()).SkipMany();
            _anyWhitespace = comments.Or(whitespaces);

            Term = new(this);
        }

        internal void SetDoucument(Core.DocumentInfo document) => _document = document;

        internal IEnumerable<Core.ObjectInfo> Parse(TextReader stream) => _documentParser.ParseOrThrow(stream);

        internal Parser<char, Core.ObjectInfo> CreateParser<T>(Parser<char, T> parser)
            where T : Core.Expression 
            => WithAnyWhitespace(
                Map((start, expr, end) => 
                    new Core.ObjectInfo(new(_document, start.Line, start.Col, end.Line, end.Col), expr),
                    Parser<char>.CurrentPos,
                    parser,
                    Parser<char>.CurrentPos
                    )
                );

        internal Parser<char, T> WithAnyWhitespace<T>(Parser<char, T> parser) => parser.Before(_anyWhitespace);
    }
}
