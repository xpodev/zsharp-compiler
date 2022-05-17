using Pidgin.Comment;
using System.Collections.Generic;
using System.Linq;
using ZSharp.Parser.Extensibility;

namespace ZSharp.Parser
{
    public class DocumentParser : ExtensibleParser<Collection, Node>
    {
#if DEBUG
        public static DocumentParser Instance { get; } = new();
#endif

        private DocumentInfo _document;
        private Parser<char, Collection> _documentParser;

        private readonly Parser<char, Unit> _anyWhitespace;

        public Symbols Symbols { get; } = new();

        public IdentifierParser Identifier { get; }

        public LiteralParser Literal { get; }

        public TermParser Term { get; }

        public ExpressionParser Expression { get; }

        public CodeBlockParser CodeBlock { get; }

        public override Parser<char, Collection> Parser => _documentParser;

        internal DocumentParser() : base("Core.Document")
        {
            Parser<char, Unit> comments, whitespace;

            comments =
                Try(CommentParser.SkipLineComment(String("//")))
                .Or(CommentParser.SkipBlockComment(String("/*"), String("*/")));

            whitespace = Whitespace.IgnoreResult().Or(EndOfLine.IgnoreResult());
            _anyWhitespace = OneOf(comments, whitespace).SkipMany();

            ParserExtensions.Symbols = Symbols;
            ParserExtensions.Parser = this;

            Identifier = new(this);
            Literal = new(this);
            Term = new(this);
            Expression = new(this);
            CodeBlock = new(this);

            Expression.Build(Term.Parser);
        }

        internal void SetDocument(DocumentInfo document) => _document = document;

        public Parser<char, NodeInfo<T>> CreateParser<T>(Parser<char, T> parser)
            where T : Node 
            => WithAnyWhitespace(
                Map((start, expr, end) => 
                    new NodeInfo<T>(new(_document, start.Line, start.Col, end.Line, end.Col), expr),
                    CurrentPos,
                    parser,
                    CurrentPos
                    )
                );

        internal Parser<char, T> WithAnyWhitespace<T>(Parser<char, T> parser) => parser.Before(_anyWhitespace);

        public void Build(Parser _)
        {
            _documentParser = 
                _anyWhitespace.Then(
                    OneOf(_extensions.Values.Select(extension => Try(extension.Parser)))
                    .BeforeWhitespace()
                    .ManyCollection()
                    ).Before(End);
        }
    }
}
