using Pidgin;
using ZSharp.Core;
using static Pidgin.Parser;
using static ZSharp.Parser.ParserState;

namespace ZSharp.Parser
{
    public static class Literals
    {
        private static readonly Parser<char, char> Non0Digit =
            OneOf('1', '2', '3', '4', '5', '6', '7', '8', '9');

        private static readonly Parser<char, char> Digit = Non0Digit.Or(Char('0'));

        private static readonly Parser<char, string> IntegralNumber =
            Map((c, cs) => string.Concat(c, cs), Non0Digit, Digit.ManyString());

        private static readonly Parser<char, string> RealNumber =
            OneOf(
                Try(Map((i, dot, f) => i + dot + f, IntegralNumber, Char('.'), IntegralNumber)),
                Try(Map((i, dot) => i + dot, IntegralNumber, Char('.'))),
                Map((dot, f) => dot + f, Char('.'), IntegralNumber)
                );

        public static readonly Parser<char, ObjectInfo> StringParser =
            AnyCharExcept('"').ManyString()
            .Between(Symbols.DoubleQuotes, Symbols.DoubleQuotes)
            .Before(Syntax.Whitespaces)
            .Select<ObjectInfo>(s => new(GetInfo(s), new Literal(s)));

        public static readonly Parser<char, ObjectInfo> CharacterParser =
            Parser<char>.Any
            .Between(Symbols.SingleQuote, Symbols.SingleQuote)
            .Before(Syntax.Whitespaces)
            .Select<ObjectInfo>(c => new(GetInfo(c.ToString()), new Literal(c)));

        public static readonly Parser<char, ObjectInfo> IntegerParser =
            Map<char, string, string, ObjectInfo>((string num, string fmt) => fmt switch
            {
                "i8" => new(GetInfo(num + "i8"), new Literal(sbyte.Parse(num))),
                "u8" => new(GetInfo(num + "u8"), new Literal(byte.Parse(num))),
                "i16" => new(GetInfo(num + "i16"), new Literal(short.Parse(num))),
                "u16" => new(GetInfo(num + "u16"), new Literal(ushort.Parse(num))),
                "i32" => new(GetInfo(num + "i32"), new Literal(int.Parse(num))),
                "u32" => new(GetInfo(num + "u32"), new Literal(uint.Parse(num))),
                "i64" => new(GetInfo(num + "i64"), new Literal(long.Parse(num))),
                "u64" => new(GetInfo(num + "u64"), new Literal(ulong.Parse(num))),
                "N" => new(GetInfo(num + "N"), new Literal(nint.Parse(num))),
                "U" => new(GetInfo(num + "U"), new Literal(nuint.Parse(num))),
                _ => new(GetInfo(num), new Literal(int.Parse(num))),
            }, 
                IntegralNumber, 
                OneOf(
                    Try(String("i8")),
                    Try(String("u8")),
                    Try(String("i16")),
                    Try(String("u16")),
                    Try(String("i32")),
                    Try(String("u32")),
                    Try(String("i64")),
                    Try(String("u64")),
                    Try(String("N")),
                    Try(String("U")),
                    String(string.Empty)
                )).Before(Syntax.Whitespaces);

        public static readonly Parser<char, ObjectInfo> RealParser =
            Map<char, string, string, ObjectInfo>((num, fmt) => fmt switch
            {
                "f32" => new(GetInfo(num + "f32"), new Literal(float.Parse(num))),
                "f64" => new(GetInfo(num + "f64"), new Literal(double.Parse(num))),
                _ => new(GetInfo(num), new Literal(double.Parse(num)))
            }, RealNumber,
                OneOf(
                    Try(String("f32")),
                    Try(String("f64")),
                    String(string.Empty)
                    )).Before(Syntax.Whitespaces);

        public static readonly Parser<char, ObjectInfo> Parser =
            OneOf(
                StringParser,
                CharacterParser,
                IntegerParser,
                RealParser
                );
    }
}
