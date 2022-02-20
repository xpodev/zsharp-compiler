using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using Pidgin.Expression;
using ZSharp.OldCore;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
using static ZSharp.Parser.ParserState;

namespace ZSharp.Parser
{
    public static class Expression
    {
        private static bool _isExpressionParserBuilt = false, _isTermParserBuilt = false;

        private static Parser<char, ObjectInfo> _single, _many, _termParser;

        private static readonly SortedDictionary<int, OperatorTableRow<char, ObjectInfo>> _operatorTable = new()
        {
            //{ 10, Operator.Postfix(FunctionCall.TupleCall(Rec(() => Parser))) },
            //{ 60, Operator.Postfix(FunctionCall.InitializerCall(Rec(() => Parser))) },
        };

        private static readonly List<Parser<char, ObjectInfo>> _terms = new()
        {
            Try(Rec(() => Single).Between(Symbols.LCurvyBracket, Symbols.RCurvyBracket)),
            Try(Literals.Parser),
            Try(CreateFileInfo(Identifier.Parser.Select<OldCore.Expression>(id => new OldCore.Identifier(id))))
        };

        private static readonly List<string> _keywords = new();

        private static void AddOperator(OperatorTableRow<char, ObjectInfo> row, int precedence)
        {
            _isExpressionParserBuilt = false;
            if (!_operatorTable.ContainsKey(precedence))
            {
                _operatorTable.Add(precedence, row);
            }
            else
            {
                _operatorTable[precedence] = _operatorTable[precedence].And(row);
            }
        }

        private static Parser<char, ObjectInfo> CreateTermParser()
        {
            if (!_isTermParserBuilt)
            {
                _isTermParserBuilt = true;
                _termParser = OneOf(_terms);
            }
            return _termParser;
        }

        private static void CreateParser()
        {
            if (!_isExpressionParserBuilt)
            {
                _isExpressionParserBuilt = true;
                _single = ExpressionParser.Build(CreateTermParser(), _operatorTable.Values);
                _many = _single.Many().Select<ObjectInfo>(exprs =>
                    new(FileInfo.Combine(exprs.First().FileInfo, exprs.Last().FileInfo), new Collection(exprs))
                    );
            }
        }

        private static void AddOperator<T>(
            string name,
            Parser<char, T> parser,
            OperatorFixity fixity,
            int precedence,
            Associativity associativity = Associativity.None
            )
        {
            OperatorTableRow<char, ObjectInfo> row = fixity switch
            {
                OperatorFixity.Prefix =>
                    Operator.PrefixChainable(
                        parser.WithResult<Func<ObjectInfo, ObjectInfo>>(
                            operand => new(
                                operand.FileInfo,
                                new UnaryExpression(
                                    operand,
                                    name + '_',
                                    true
                                    )
                                )
                            )
                        ),
                OperatorFixity.Infix =>
                    Operator.Binary(
                        (BinaryOperatorType)associativity,
                        parser.WithResult<Func<ObjectInfo, ObjectInfo, ObjectInfo>>(
                            (left, right) => new(
                                FileInfo.Combine(left.FileInfo, right.FileInfo),
                                new BinaryExpression(
                                    left, 
                                    right,
                                    '_' + name + '_'
                                    )
                                )
                            )
                        ),
                OperatorFixity.Postfix =>
                        Operator.PostfixChainable(
                            parser.WithResult<Func<ObjectInfo, ObjectInfo>>(
                                operand => new(
                                    operand.FileInfo,
                                    new UnaryExpression(
                                        operand,                                    
                                        '_' + name, 
                                        false
                                        )
                                    )
                                )
                            )
            };
            AddOperator(row, precedence);
        }

        public static void AddKeywordTerm(string kw)
        {
            _keywords.Add(kw);
            Parser<char, string> parser;
            if (!kw.ToLower().All(c => 'a' <= c && c <= 'z')) parser = String(kw).Before(Syntax.Whitespaces);
            else parser = Identifier.Parser.Assert(s => s == kw);
            _isExpressionParserBuilt = _isTermParserBuilt = false;
            _terms.Insert(0, CreateFileInfo(Try(parser).Select<OldCore.Expression>(s => new Keyword(s))));
        }

        public static void AddKeywordOperator(string kw, int precedence, OperatorFixity fixity)
        {
            _keywords.Add(kw);
            AddOperator(
                kw,
                Try(Identifier.Parser.Assert(s => s == kw).Before(Syntax.Whitespaces)),
                fixity,
                precedence,
                Associativity.Left
                );
        }

        public static void AddBinaryOperator(string op, int precedence, Associativity associativity)
        {
            AddOperator(op, Syntax.String(op), OperatorFixity.Infix, precedence, associativity);
        }

        public static void AddPrefixOperator(string op, int precedence)
        {
            AddOperator(op, Syntax.String(op), OperatorFixity.Prefix, precedence);
        }

        public static void AddPostfixOperator(string op, int precedence)
        {
            AddOperator(op, Syntax.String(op), OperatorFixity.Postfix, precedence);
        }

        public static void AddSurroundingOperator(string left, string right, int precedence,
            bool isPrefix, bool allowMultiple = false)
        {
            string name = left + '_' + right;
            name = isPrefix ? name + '_' : '_' + name;
            Parser<char, Func<ObjectInfo, ObjectInfo>> parser =
                Try(CreateFileInfo(Rec(() => Return<OldCore.Expression>(Collection.Empty)))
                .Between(Syntax.String(left), Syntax.String(right)))
                .Or(
                    Rec(() => allowMultiple ? Many : Single)
                    .Between(Syntax.String(left), Syntax.String(right))
                    )
                .Select<Func<ObjectInfo, ObjectInfo>>
                (args => method => new(
                    FileInfo.Combine(method.FileInfo, args.FileInfo),
                    new FunctionCall(method, args)
                    {
                        Name = name,
                        IsPrefix = isPrefix
                    })
                );

            OperatorTableRow<char, ObjectInfo> row =
                isPrefix ? Operator.PrefixChainable(parser) : Operator.PostfixChainable(parser);
            AddOperator(row, precedence);
        }

        public static Parser<char, ObjectInfo> Many
        {
            get
            {
                CreateParser();
                return _many;
            }
        }

        public static Parser<char, ObjectInfo> Single
        {
            get
            {
                CreateParser();
                return _single;
            }
        }
    }
}
