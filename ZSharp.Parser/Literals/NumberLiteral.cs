using System;
using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
using ZSharp.Core;

namespace ZSharp.Parser
{
    public class NumberLiteral
    {
        /// <summary>
        /// Signed byte (sbyte) postfix
        /// </summary>
        public const string I1 = "i8";

        /// <summary>
        /// Unsigned byte (byte) postfix
        /// </summary>
        public const string U1 = "u8";

        /// <summary>
        /// Signed short (short) postfix
        /// </summary>
        public const string I2 = "i16";

        /// <summary>
        /// Unsigned short (ushort) postfix
        /// </summary>
        public const string U2 = "u16";

        /// <summary>
        /// Signed int (int) postfix
        /// </summary>
        public const string I4 = "i32";

        /// <summary>
        /// Unsigned int (uint) postfix
        /// </summary>
        public const string U4 = "u32";

        /// <summary>
        /// Signed long (long) postfix
        /// </summary>
        public const string I8 = "i64";

        /// <summary>
        /// Unsigned long (ulong) postfix
        /// </summary>
        public const string U8 = "u64";

        /// <summary>
        /// Float 32 (single/float) postfix
        /// </summary>
        public const string R4 = "f32";

        /// <summary>
        /// Float 64 (double) postfix
        /// </summary>
        public const string R8 = "f64";

        /// <summary>
        /// Native int (nint) postfix
        /// </summary>
        public const string IN = "I";

        /// <summary>
        /// Native unsigned int (nuint) postfix
        /// </summary>
        public const string UN = "U";

        public Parser<char, Literal> Integer { get; }

        public Parser<char, Literal> Real { get; }

        private static Parser<char, object> CreateIntegerParser(Parser<char, string> parser, Parser<char, string> sign, int @base) =>
            Map((string s, string num, string fmt) =>
            {
                bool neg = s == "-";
                object value = fmt switch
                {
                    I1 => (sbyte)(neg ? -Convert.ToSByte(num, @base) : Convert.ToSByte(num, @base)),
                    U1 => Convert.ToByte(num, @base),
                    I2 => (short)(neg ? -Convert.ToInt16(num, @base) : Convert.ToInt16(num, @base)),
                    U2 => Convert.ToUInt16(num, @base),
                    I4 => neg ? -Convert.ToInt32(num, @base) : Convert.ToInt32(num, @base),
                    U4 => Convert.ToUInt32(num, @base),
                    I8 => neg ? -Convert.ToInt64(num, @base) : Convert.ToInt64(num, @base),
                    U8 => Convert.ToUInt64(num, @base),
                    IN => (nint)(neg ? -Convert.ToInt64(num, @base) : Convert.ToInt64(num, @base)),
                    UN => (nuint)Convert.ToUInt64(num, @base),
                    _ => (object)(neg ? -Convert.ToInt32(num, @base) : Convert.ToInt32(num, @base))
                };
                return value;
            },
                sign,
                parser,
                OneOf(
                    Try(String(I1)),
                    Try(String(U1)),
                    Try(String(I2)),
                    Try(String(U2)),
                    Try(String(I4)),
                    Try(String(U4)),
                    Try(String(I8)),
                    Try(String(U8)),
                    Try(String(IN)),
                    Try(String(UN)),
                    String(string.Empty)
                ));

        public NumberLiteral()
        {
            Parser<char, char> zero, nonZeroDigit, digit;
            zero = Char('0');
            nonZeroDigit = OneOf("123456789");
            digit = zero.Or(nonZeroDigit);
            Parser<char, string> sign = Try(String("-").Or(Char('+').ThenReturn(string.Empty))).Or(Return(string.Empty));

            Parser<char, string> dec = 
                Map(string.Concat, nonZeroDigit.Select(s => s.ToString()), digit.ManyString()).Or(zero.Select(s => s.ToString()));

            Parser<char, string> hex =
                Map(string.Concat, zero.Then(Char('x')).Then(digit.Or(CIOneOf("ABCDEF")).ManyString()));

            Parser<char, string> octal =
                Map(string.Concat, zero.Then(Char('o')).Then(OneOf("01234567").ManyString()));

            Integer = OneOf(
                Try(CreateIntegerParser(octal, sign, 8)),
                Try(CreateIntegerParser(hex, sign, 16)),
                CreateIntegerParser(dec, sign, 10)
                ).Select(o => new Literal(o));

            Parser<char, string> real =
                Map(string.Concat,
                    sign,
                    OneOf(
                        Try(Map((i, dot, f) => i + dot + f, dec, Char('.'), digit.ManyString())),
                        Try(Map((i, dot) => i + dot, dec, Char('.'))),
                        Map((dot, f) => dot + f, Char('.'), digit.ManyString())
                        )
                    );

            Real =
                Map(
                    (num, fmt) => fmt switch
                    {
                        R4 => Convert.ToSingle(num),
                        R8 => Convert.ToDouble(num),
                        _ => (object)Convert.ToDouble(num),
                    }, 
                    real,
                    OneOf(
                        Try(String(R4)),
                        Try(String(R8)),
                        String(string.Empty)
                        )
                    ).Select(o => new Literal(o));
        }
    }
}
