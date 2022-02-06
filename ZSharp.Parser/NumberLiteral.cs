using System;
using Pidgin;
using static Pidgin.Parser;
using ZSharp.Core;

namespace ZSharp.Parser
{
    internal class NumberLiteral
    {
        /// <summary>
        /// Signed byte (sbyte) postfix
        /// </summary>
        const string I1 = "i8";

        /// <summary>
        /// Unsigned byte (byte) postfix
        /// </summary>
        const string U1 = "u8";

        /// <summary>
        /// Signed short (short) postfix
        /// </summary>
        const string I2 = "i16";

        /// <summary>
        /// Unsigned short (ushort) postfix
        /// </summary>
        const string U2 = "u16";

        /// <summary>
        /// Signed int (int) postfix
        /// </summary>
        const string I4 = "i32";

        /// <summary>
        /// Unsigned int (uint) postfix
        /// </summary>
        const string U4 = "u32";

        /// <summary>
        /// Signed long (long) postfix
        /// </summary>
        const string I8 = "i64";

        /// <summary>
        /// Unsigned long (ulong) postfix
        /// </summary>
        const string U8 = "u64";

        /// <summary>
        /// Float 32 (single/float) postfix
        /// </summary>
        const string R4 = "f32";

        /// <summary>
        /// Float 64 (double) postfix
        /// </summary>
        const string R8 = "f64";

        /// <summary>
        /// Native int (nint) postfix
        /// </summary>
        const string IN = "I";

        /// <summary>
        /// Native unsigned int (nuint) postfix
        /// </summary>
        const string UN = "U";

        internal Parser<char, Literal> Integer { get; }

        internal Parser<char, Literal> Real { get; }

        private static Parser<char, object> CreateIntegerParser(Parser<char, string> parser, int @base) =>
            Map<char, string, string, object>((string num, string fmt) => fmt switch
            {
                I1 => Convert.ToSByte(num, @base),
                U1 => Convert.ToByte(num, @base),
                I2 => Convert.ToInt16(num, @base),
                U2 => Convert.ToUInt16(num, @base),
                I4 => Convert.ToInt32(num, @base),
                U4 => Convert.ToUInt32(num, @base),
                I8 => Convert.ToInt64(num, @base),
                U8 => Convert.ToUInt64(num, @base),
                IN => (nint)Convert.ToInt64(num, @base),
                UN => (nuint)Convert.ToUInt64(num, @base),
                _ => Convert.ToInt32(num, @base),
            },
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

        internal NumberLiteral()
        {
            Parser<char, char> zero, nonZeroDigit, digit;
            zero = Char('0');
            nonZeroDigit = OneOf("123456789");
            digit = zero.Or(nonZeroDigit);
            Parser<char, string> sign = Try(String("-").Or(Char('+').ThenReturn(string.Empty)));

            Parser<char, string> @decimal = 
                Map(string.Concat, sign, nonZeroDigit.Cast<string>(), digit.ManyString()).Or(zero.Cast<string>());

            Parser<char, string> hex =
                Map(string.Concat, sign, zero.Then(Char('x')).Then(digit.Or(CIOneOf("ABCDEF")).ManyString()));

            Parser<char, string> octal =
                Map(string.Concat, sign, zero.Then(Char('o')).Then(OneOf("01234567").ManyString()));

            Integer = OneOf(
                CreateIntegerParser(@decimal, 10),
                CreateIntegerParser(hex, 16),
                CreateIntegerParser(octal, 8)
                ).Select(o => new Literal(o));

            Parser<char, string> real =
                Map(string.Concat,
                    sign,
                    OneOf(
                        Try(Map((i, dot, f) => i + dot + f, @decimal, Char('.'), @decimal)),
                        Try(Map((i, dot) => i + dot, @decimal, Char('.'))),
                        Map((dot, f) => dot + f, Char('.'), @decimal)
                        )
                    );

            Real =
                Map<char, string, string, object>(
                    (num, fmt) => fmt switch
                    {
                        R4 => Convert.ToSingle(num),
                        R8 => Convert.ToDouble(num),
                        _ => Convert.ToDouble(num),
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
