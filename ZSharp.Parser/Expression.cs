using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using Pidgin.Expression;
using ZSharp.Core;
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
            Try(Identifier.Parser.Select<ObjectInfo>(id => new(GetInfo(id), new Core.Identifier(id))))
        };

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
            bool isKeyword,
            Associativity associativity = Associativity.None
            )
        {
            OperatorTableRow<char, ObjectInfo> row = fixity switch
            {
                OperatorFixity.Prefix =>
                    Operator.Prefix(
                        parser.WithResult<Func<ObjectInfo, ObjectInfo>>(
                            operand => new(
                                operand.FileInfo,
                                new UnaryExpression(
                                    operand,
                                    isKeyword ? new KeywordName(name) : new OperatorName(name),
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
                                    isKeyword ? new KeywordName(name) : new OperatorName(name)
                                    )
                                )
                            )
                        ),
                OperatorFixity.Postfix =>
                        Operator.Postfix(
                            parser.WithResult<Func<ObjectInfo, ObjectInfo>>(
                                operand => new(
                                    operand.FileInfo,
                                    new UnaryExpression(
                                        operand,                                    
                                        isKeyword ? new KeywordName(name) : new OperatorName(name), 
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
            Parser<char, string> parser;
            if (!kw.ToLower().All(c => 'a' <= c && c <= 'z')) parser = String(kw).Before(Syntax.Whitespaces);
            else parser = Identifier.Parser.Assert(s => s == kw);
            _isExpressionParserBuilt = _isTermParserBuilt = false;
            _terms.Insert(0, Try(parser).Select<ObjectInfo>(s => new(GetInfo(s), new KeywordName(s))));
        }

        public static void AddKeywordOperator(string kw, int precedence, OperatorFixity fixity)
        {
            AddOperator(
                kw,
                Try(Identifier.Parser.Assert(s => s == kw).Select<ObjectInfo>(s => new(GetInfo(s), new KeywordName(s))).Before(Syntax.Whitespaces)),
                fixity,
                precedence,
                true,
                Associativity.Left
                );
        }

        public static void AddBinaryOperator(string op, int precedence, Core.Associativity associativity,
            bool isKeyword = false)
        {
            OperatorTableRow<char, ObjectInfo> row =
                Operator.Binary(
                    (BinaryOperatorType)associativity,
                    Try(String(op).Before(Syntax.Whitespaces).ThenReturn<Func<ObjectInfo, ObjectInfo, ObjectInfo>>(
                        (left, right) => new(
                            FileInfo.Combine(left.FileInfo, right.FileInfo),
                            new BinaryExpression(
                                left,
                                right,
                                isKeyword ? new KeywordName(op) : new OperatorName(op)
                                )
                            )
                        )
                    )
                 );
            AddOperator(row, precedence);
        }

        public static void AddPrefixOperator(string op, int precedence, bool isKeyword = false)
        {
            OperatorTableRow<char, ObjectInfo> row =
                Operator.PrefixChainable(
                    Try(
                        Syntax.String(op).Select<Func<ObjectInfo, ObjectInfo>>(
                            @operator => operand => new(
                                FileInfo.Combine(@operator, operand.FileInfo), 
                                new UnaryExpression(
                                    operand, 
                                    isKeyword ? new KeywordName(op) : new OperatorName(op),
                                    true
                                )
                            )
                        )
                    )
                 );
            AddOperator(row, precedence);
        }

        public static void AddPostfixOperator(string op, int precedence, bool isKeyword = false)
        {
            OperatorTableRow<char, ObjectInfo> row =
                Operator.PostfixChainable(
                    Try(
                        Syntax.String(op).Select<Func<ObjectInfo, ObjectInfo>>(
                            @operator => operand => new(
                                FileInfo.Combine(@operator, operand.FileInfo),
                                new UnaryExpression(
                                    operand,
                                    isKeyword ? new KeywordName(op) : new OperatorName(op),
                                    false
                                )
                            )
                        )
                    )
                 );
            AddOperator(row, precedence);
        }

        public static void AddSurroundingOperator(string left, string right, int precedence,
            bool isPrefix, bool allowMultiple = false)
        {
            string name = left + "operator" + right;
            name = isPrefix ? "operator" + name : name + "operator";
            Parser<char, Func<ObjectInfo, ObjectInfo>> parser =
                Try(Return<ObjectInfo>(new(GetInfo(string.Empty), Collection.Empty))
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

            OperatorTableRow<char, ObjectInfo> row = isPrefix
                ? Operator.PrefixChainable(parser)
                : Operator.PostfixChainable(parser);
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
