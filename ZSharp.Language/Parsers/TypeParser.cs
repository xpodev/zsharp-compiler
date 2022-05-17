using Pidgin;
using System.Linq;
using ZSharp.Parser;
using static Pidgin.Parser;

namespace ZSharp.Language
{
    public class TypeParser : CustomParser<TypeNode>
    {
        private Parser<char, FunctionTypeNode> _functionType;
        private Parser<char, TupleType> _tupleType;
        private Parser<char, TypeNameNode> _typeName;
        private Parser<char, AutoTypeNode> _autoType;

        public Parser<char, TypeNode> Type => Rec(() => Parser);

        public Parser<char, FunctionTypeNode> FunctionType => _functionType;

        public Parser<char, TupleType> TupleType => _tupleType;

        public Parser<char, TypeNameNode> TypeName => _typeName;

        public Parser<char, AutoTypeNode> AutoType => _autoType;

        public TypeParser() : base("Type", "<ZSharp>")
        {
        }

        public TypeParser Build(Parser.Parser parser)
        {
            Parser<char, NodeInfo<TypeNode>> type = Type.WithObjectInfo();

            _autoType = Parser<char>.Return(TypeNode.Infer).WithPrefixKeyword("auto");

            _tupleType =
                from types in type.Separated(parser.Document.Symbols.Comma).Parenthesized()
                select types.Any() ? new TupleType(types) : TypeNode.Unit;

            _typeName =
                from parts in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Dot)
                from typeArguments in type.Separated(parser.Document.Symbols.Comma).Parenthesized(BracketType.Angle).Optional()
                select new TypeNameNode(new(parts), typeArguments.GetValueOrDefault(System.Array.Empty<NodeInfo<TypeNode>>()));

            _functionType =
                from input in _tupleType.Cast<TypeNode>().WithObjectInfo()
                from _ in parser.Document.Symbols.Symbol("->")
                from output in type
                select new FunctionTypeNode(input, output);

            Parser = OneOf(
                Try(_functionType.Cast<TypeNode>()),
                _tupleType.Cast<TypeNode>(),
                Try(_autoType.Cast<TypeNode>()),
                _typeName.Cast<TypeNode>()
                );

            return this;
        }
    }
}
