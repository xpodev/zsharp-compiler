using Pidgin;
using ZSharp.OldCore;
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
            Map((c, cs) => string.Concat(c, cs), Non0Digit, Digit.ManyString()).Or(String("0"));

        private static readonly Parser<char, string> RealNumber =
            OneOf(
                Try(Map((i, dot, f) => i + dot + f, IntegralNumber, Char('.'), IntegralNumber)),
                Try(Map((i, dot) => i + dot, IntegralNumber, Char('.'))),
                Map((dot, f) => dot + f, Char('.'), IntegralNumber)
                );

        public static readonly Parser<char, ObjectInfo> StringParser =
            CreateFileInfo(
                AnyCharExcept('"').ManyString()
                .Between(Symbols.DoubleQuotes, Symbols.DoubleQuotes)
                .Before(Syntax.Whitespaces)
                .Select<OldCore.Expression>(s => new Literal(s))
                );

        public static readonly Parser<char, ObjectInfo> CharacterParser =
            CreateFileInfo(
                Parser<char>.Any
                .Between(Symbols.SingleQuote, Symbols.SingleQuote)
                .Before(Syntax.Whitespaces)
                .Select<OldCore.Expression>(c => new Literal(c))
                );

        public static readonly Parser<char, ObjectInfo> IntegerParser =
            CreateFileInfo(
                Map<char, string, string, object>((string num, string fmt) => fmt switch
                {
                    "i8" => sbyte.Parse(num),
                    "u8" => byte.Parse(num),
                    "i16" => short.Parse(num),
                    "u16" => ushort.Parse(num),
                    "i32" => int.Parse(num),
                    "u32" => uint.Parse(num),
                    "i64" => long.Parse(num),
                    "u64" => ulong.Parse(num),
                    "N" => nint.Parse(num),
                    "U" => nuint.Parse(num),
                    _ => int.Parse(num),
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
                    )).Before(Syntax.Whitespaces)
                .Select<OldCore.Expression>(o => new Literal(o))
                );

        public static readonly Parser<char, ObjectInfo> RealParser =
            CreateFileInfo(
                Map<char, string, string, object>((num, fmt) => fmt switch
                {
                    "f32" => float.Parse(num),
                    "f64" => double.Parse(num),
                    _ => double.Parse(num)
                }, RealNumber,
                OneOf(
                    Try(String("f32")),
                    Try(String("f64")),
                    String(string.Empty)
                    )).Before(Syntax.Whitespaces)
                .Select<OldCore.Expression>(o => new Literal(o))
                );

        public static readonly Parser<char, ObjectInfo> Parser =
            OneOf(
                StringParser,
                CharacterParser,
                IntegerParser,
                RealParser
                );
    }
}
