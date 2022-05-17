using Pidgin;
using System.Linq;
using ZSharp.Parser;
using static Pidgin.Parser;

namespace ZSharp.Language
{
    public class TypeParser : CustomParser<ModifiedObject<TypeNode>>
    {
        private Parser<char, ModifiedObject<TypeNode>> _functionType;
        private Parser<char, ModifiedObject<TypeNode>> _tupleType;
        private Parser<char, ModifiedObject<TypeNode>> _typeName;
        private Parser<char, ModifiedObject<TypeNode>> _autoType;

        public Parser<char, ModifiedObject<TypeNode>> Type => Rec(() => Parser);

        public Parser<char, ModifiedObject<TypeNode>> FunctionType => _functionType;

        public Parser<char, ModifiedObject<TypeNode>> TupleType => _tupleType;

        public Parser<char, ModifiedObject<TypeNode>> TypeName => _typeName;

        public Parser<char, ModifiedObject<TypeNode>> AutoType => _autoType;

        public TypeParser() : base("Type")
        {
        }

        public TypeParser Build(Parser.Parser parser)
        {
            Parser<char, NodeInfo<ModifiedObject<TypeNode>>> type = Type.WithObjectInfo();

            _autoType = Parser<char>.Return<TypeNode>(TypeNode.Infer).WithPrefixKeyword("auto").WithPrefixModifiers();

            _tupleType = (
                from types in type.Separated(parser.Document.Symbols.Comma).Parenthesized()
                select types.Any() ? new TupleType(types) : TypeNode.Unit
                ).Cast<TypeNode>().WithPrefixModifiers();

            _typeName = (
                from parts in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Dot)
                from typeArguments in type.Separated(parser.Document.Symbols.Comma).Parenthesized(BracketType.Angle).Optional()
                select new TypeNameNode(new(parts), typeArguments.GetValueOrDefault(System.Array.Empty<NodeInfo<ModifiedObject<TypeNode>>>()))
                ).Cast<TypeNode>().WithPrefixModifiers();

            _functionType = (
                from input in _tupleType.WithObjectInfo()
                from _ in parser.Document.Symbols.Symbol("->")
                from output in type
                select new FunctionTypeNode(input, output)
                ).Cast<TypeNode>().WithPrefixModifiers();

            Parser = OneOf(
                Try(_functionType),
                _tupleType,
                Try(_autoType),
                _typeName
                );

            return this;
        }
    }
}
