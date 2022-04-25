using Pidgin;
using System.Linq;
using ZSharp.Parser;
using static Pidgin.Parser;

namespace ZSharp.Language
{
    internal class TypeParser : CustomParser<Type>
    {
        private Parser<char, FunctionType> _functionType;
        private Parser<char, TupleType> _tupleType;
        private Parser<char, TypeName> _typeName;
        private Parser<char, AutoType> _autoType;

        public Parser<char, Type> Type => Rec(() => Parser);

        public Parser<char, FunctionType> FunctionType => _functionType;

        public TypeParser() : base("Type", "<ZSharp>")
        {
        }

        public void Build(Parser.Parser parser)
        {
            Parser<char, NodeInfo<Type>> type = Type.WithObjectInfo();

            _autoType = Parser<char>.Return(Language.Type.Infer).WithPrefixKeyword("auto");

            _tupleType =
                from types in type.Separated(parser.Document.Symbols.Comma).Parenthesized()
                select types.Any() ? new TupleType(types) : Language.Type.Unit;

            _typeName =
                from parts in parser.Document.Identifier.Parser.Separated(parser.Document.Symbols.Dot)
                from typeArguments in type.Separated(parser.Document.Symbols.Comma).Parenthesized(BracketType.Angle).Optional()
                select new TypeName(new(parts), typeArguments.GetValueOrDefault(System.Array.Empty<NodeInfo<Type>>()));

            _functionType =
                from input in _tupleType.Cast<Type>().WithObjectInfo()
                from _ in parser.Document.Symbols.Symbol("->")
                from output in type
                select new FunctionType(input, output);

            Parser = OneOf(
                Try(_functionType.Cast<Type>()),
                _tupleType.Cast<Type>(),
                Try(_autoType.Cast<Type>()),
                _typeName.Cast<Type>()
                );
        }
    }
}
