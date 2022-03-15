using Pidgin.Expression;
using System;
using System.Collections.Generic;
using ZSharp.Parser.Extensibility;

namespace ZSharp.Parser
{
    using ExpressionInfo = NodeInfo<Expression>;

    public class ExpressionParser
    {
        private readonly HashSet<string> _registeredOperatorSymbols = new();

        private readonly SortedDictionary<int, OperatorTableRow<char, ExpressionInfo>> _operators = new()
        {

        };

        private readonly DocumentParser _documentParser;

        public Parser<char, ExpressionInfo> Parser { get; private set; }

        internal ExpressionParser(DocumentParser documentParser)
        {
            _documentParser = documentParser;
        }

        public void RegisterCustomOperator(CustomOperator customOperator)
        {
            if (_registeredOperatorSymbols.Contains(customOperator.Symbol))
                throw new InvalidOperationException("Operator \'{customOperator.Symbol}\' is already registered");

            if (!_operators.ContainsKey(customOperator.Precedence))
                _operators.Add(customOperator.Precedence, GetOperatorParser(customOperator));
            else
                _operators[customOperator.Precedence] = _operators[customOperator.Precedence].And(GetOperatorParser(customOperator));
        }

        private OperatorTableRow<char, ExpressionInfo> GetOperatorParser(CustomOperator op) =>
            op.Type switch
            {
                OperatorType.Infix => Operator.InfixN(
                                        String(op.Symbol).ThenReturn(
                                            (ExpressionInfo l, ExpressionInfo r) =>
                                            new ExpressionInfo(FileInfo.Combine(l.FileInfo, r.FileInfo), new BinaryExpression(l, r, op.Symbol))
                                            )
                                        ),
                OperatorType.InfixL => Operator.InfixL(
              String(op.Symbol).ThenReturn(
                  (ExpressionInfo l, ExpressionInfo r) =>
                  new ExpressionInfo(FileInfo.Combine(l.FileInfo, r.FileInfo), new BinaryExpression(l, r, op.Symbol))
                  )
              ),
                OperatorType.InfixR => Operator.InfixR(
              String(op.Symbol).ThenReturn(
                  (ExpressionInfo l, ExpressionInfo r) =>
                  new ExpressionInfo(FileInfo.Combine(l.FileInfo, r.FileInfo), new BinaryExpression(l, r, op.Symbol))
                  )
              ),
                OperatorType.Postfix => Operator.Postfix(
                    String(op.Symbol).ThenReturn(
                        (ExpressionInfo expr) =>
                        new ExpressionInfo(expr.FileInfo, new UnaryExpression(expr, op.Symbol, false))
                    )
              ),
                OperatorType.Prefix => Operator.Prefix(
                    String(op.Symbol).ThenReturn(
                            (ExpressionInfo expr) =>
                            new ExpressionInfo(expr.FileInfo, new UnaryExpression(expr, op.Symbol, true))
                        )
              ),
                _ => throw new ArgumentOutOfRangeException(nameof(op.Type), op.Type, $"Value not defined in enum {nameof(OperatorType)}"),
            };

        internal void Build(Parser<char, ExpressionInfo> term)
        {
            Parser = Pidgin.Expression.ExpressionParser.Build(term, _operators.Values).BeforeWhitespace();
        }
    }
}
