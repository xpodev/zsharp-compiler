using System;

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

        #region Integer Decimal Parsers

        public Parser<char, NodeInfo<Literal<sbyte>>> SInt8 { get; }
        public Parser<char, NodeInfo<Literal<byte>>> UInt8 { get; }
        public Parser<char, NodeInfo<Literal<short>>> SInt16 { get; }
        public Parser<char, NodeInfo<Literal<ushort>>> UInt16 { get; }
        public Parser<char, NodeInfo<Literal<int>>> SInt32 { get; }
        public Parser<char, NodeInfo<Literal<uint>>> UInt32 { get; }
        public Parser<char, NodeInfo<Literal<long>>> SInt64 { get; }
        public Parser<char, NodeInfo<Literal<ulong>>> UInt64 { get; }
        public Parser<char, NodeInfo<Literal<nint>>> SIntN { get; }
        public Parser<char, NodeInfo<Literal<nuint>>> UIntN { get; }

        #endregion

        #region Integer HexaDecimal Parsers

        public Parser<char, NodeInfo<Literal<sbyte>>> SInt8Hex { get; }
        public Parser<char, NodeInfo<Literal<byte>>> UInt8Hex { get; }
        public Parser<char, NodeInfo<Literal<short>>> SInt16Hex { get; }
        public Parser<char, NodeInfo<Literal<ushort>>> UInt16Hex { get; }
        public Parser<char, NodeInfo<Literal<int>>> SInt32Hex { get; }
        public Parser<char, NodeInfo<Literal<uint>>> UInt32Hex { get; }
        public Parser<char, NodeInfo<Literal<long>>> SInt64Hex { get; }
        public Parser<char, NodeInfo<Literal<ulong>>> UInt64Hex { get; }
        public Parser<char, NodeInfo<Literal<nint>>> SIntNHex { get; }
        public Parser<char, NodeInfo<Literal<nuint>>> UIntNHex { get; }

        #endregion

        #region Integer Octal Parsers

        public Parser<char, NodeInfo<Literal<sbyte>>> SInt8Octal { get; }
        public Parser<char, NodeInfo<Literal<byte>>> UInt8Octal { get; }
        public Parser<char, NodeInfo<Literal<short>>> SInt16Octal { get; }
        public Parser<char, NodeInfo<Literal<ushort>>> UInt16Octal { get; }
        public Parser<char, NodeInfo<Literal<int>>> SInt32Octal { get; }
        public Parser<char, NodeInfo<Literal<uint>>> UInt32Octal { get; }
        public Parser<char, NodeInfo<Literal<long>>> SInt64Octal { get; }
        public Parser<char, NodeInfo<Literal<ulong>>> UInt64Octal { get; }
        public Parser<char, NodeInfo<Literal<nint>>> SIntNOctal { get; }
        public Parser<char, NodeInfo<Literal<nuint>>> UIntNOctal { get; }

        #endregion

        #region Integer Parsers

        public Parser<char, NodeInfo<Literal>> IntegerDecimal { get; }
        public Parser<char, NodeInfo<Literal>> IntegerHex { get; }
        public Parser<char, NodeInfo<Literal>> IntegerOctal { get; }

        #endregion

        #region Real Parsers

        public Parser<char, NodeInfo<Literal<float>>> Float32 { get; }
        public Parser<char, NodeInfo<Literal<double>>> Float64 { get; }

        #endregion

        public Parser<char, NodeInfo<Literal>> Integer { get; }

        public Parser<char, NodeInfo<Literal>> Real { get; }

        public Parser<char, NodeInfo<Literal>> Parser { get; }

        private static Parser<char, Literal<T>> CreateIntegerParser<T>(
            Parser<char, string> parser, 
            Parser<char, string> sign, 
            int @base, 
            Func<string, int, T> convertPositive,
            Func<string, int, T> convertNegative,
            string fmt = null) =>
                Map(
                    (s, num, _) =>
                    {
                        return new Literal<T>(s == "-" ? convertNegative(num, @base) : convertPositive(num, @base));
                    },
                    sign,
                    parser,
                    String(fmt)
                    );

        public NumberLiteral(DocumentParser doc)
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

            // Integer Decimals
            {
                SInt8 = doc.CreateParser(CreateIntegerParser(dec, sign, 10, Convert.ToSByte, (n, b) => (sbyte)-Convert.ToSByte(n, b), I1));
                UInt8 = doc.CreateParser(CreateIntegerParser(dec, sign, 10, Convert.ToByte, (n, b) => (byte)-Convert.ToByte(n, b), U1));
                SInt16 = doc.CreateParser(CreateIntegerParser(dec, sign, 10, Convert.ToInt16, (n, b) => (short)-Convert.ToInt16(n, b), I2));
                UInt16 = doc.CreateParser(CreateIntegerParser(dec, sign, 10, Convert.ToUInt16, (n, b) => (ushort)-Convert.ToUInt16(n, b), U2));
                SInt32 = doc.CreateParser(OneOf(
                    Try(CreateIntegerParser(dec, sign, 10, Convert.ToInt32, (n, b) => -Convert.ToInt32(n, b), I4)),
                    CreateIntegerParser(dec, sign, 10, Convert.ToInt32, (n, b) => -Convert.ToInt32(n, b), string.Empty)
                    ));
                UInt32 = doc.CreateParser(CreateIntegerParser(dec, sign, 10, Convert.ToUInt32, (n, b) => (uint)-Convert.ToUInt32(n, b), U4));
                SInt64 = doc.CreateParser(CreateIntegerParser(dec, sign, 10, Convert.ToInt64, (n, b) => -Convert.ToInt64(n, b), I8));
                UInt64 = doc.CreateParser(dec.Before(String(U8)).Select(Convert.ToUInt64).Select<Literal<ulong>>(n => new(n)));
                SIntN = doc.CreateParser(CreateIntegerParser(dec, sign, 10, (n, b) => (nint)Convert.ToInt64(n, b), (n, b) => (nint)(-Convert.ToInt64(n, b)), IN));
                UIntN = doc.CreateParser(dec.Before(String(UN)).Select(Convert.ToUInt64).Select<Literal<nuint>>(n => new((nuint)n)));
                IntegerDecimal = OneOf(
                    Try(SInt8.Select(v => v.With<Literal>())),
                    Try(UInt8.Select(v => v.With<Literal>())),
                    Try(SInt16.Select(v => v.With<Literal>())),
                    Try(UInt16.Select(v => v.With<Literal>())),
                    Try(UInt32.Select(v => v.With<Literal>())),
                    Try(SInt64.Select(v => v.With<Literal>())),
                    Try(UInt64.Select(v => v.With<Literal>())),
                    Try(SIntN.Select(v => v.With<Literal>())),
                    Try(UIntN.Select(v => v.With<Literal>())),
                    SInt32.Select(v => v.With<Literal>())
                    );
            }

            // Integer Hex
            {
                SInt8Hex = doc.CreateParser(CreateIntegerParser(hex, sign, 16, Convert.ToSByte, (n, b) => (sbyte)-Convert.ToSByte(n, b), I1));
                UInt8Hex = doc.CreateParser(CreateIntegerParser(hex, sign, 16, Convert.ToByte, (n, b) => (byte)-Convert.ToByte(n, b), U1));
                SInt16Hex = doc.CreateParser(CreateIntegerParser(hex, sign, 16, Convert.ToInt16, (n, b) => (short)-Convert.ToInt16(n, b), I2));
                UInt16Hex = doc.CreateParser(CreateIntegerParser(hex, sign, 16, Convert.ToUInt16, (n, b) => (ushort)-Convert.ToUInt16(n, b), U2));
                SInt32Hex = doc.CreateParser(OneOf(
                    Try(CreateIntegerParser(hex, sign, 16, Convert.ToInt32, (n, b) => -Convert.ToInt32(n, b), I4)),
                    CreateIntegerParser(hex, sign, 16, Convert.ToInt32, (n, b) => -Convert.ToInt32(n, b), string.Empty)
                    ));
                UInt32Hex = doc.CreateParser(CreateIntegerParser(hex, sign, 16, Convert.ToUInt32, (n, b) => (uint)-Convert.ToUInt32(n, b), U4));
                SInt64Hex = doc.CreateParser(CreateIntegerParser(hex, sign, 16, Convert.ToInt64, (n, b) => -Convert.ToInt64(n, b), I8));
                UInt64Hex = doc.CreateParser(hex.Before(String(U8)).Select(s => Convert.ToUInt64(s, 16)).Select<Literal<ulong>>(n => new(n)));
                SIntNHex = doc.CreateParser(CreateIntegerParser(hex, sign, 16, (n, b) => (nint)Convert.ToInt64(n, b), (n, b) => (nint)(-Convert.ToInt64(n, b)), IN));
                UIntNHex = doc.CreateParser(hex.Before(String(UN)).Select(s => Convert.ToUInt64(s, 16)).Select<Literal<nuint>>(n => new((nuint)n)));
                IntegerHex = OneOf(
                    Try(SInt8Hex.Select(v => v.With<Literal>())),
                    Try(UInt8Hex.Select(v => v.With<Literal>())),
                    Try(SInt16Hex.Select(v => v.With<Literal>())),
                    Try(UInt16Hex.Select(v => v.With<Literal>())),
                    Try(UInt32Hex.Select(v => v.With<Literal>())),
                    Try(SInt64Hex.Select(v => v.With<Literal>())),
                    Try(UInt64Hex.Select(v => v.With<Literal>())),
                    Try(SIntNHex.Select(v => v.With<Literal>())),
                    Try(UIntNHex.Select(v => v.With<Literal>())),
                    SInt32Hex.Select(v => v.With<Literal>())
                    );
            }

            // Integer Octal
            {
                SInt8Octal = doc.CreateParser(CreateIntegerParser(octal, sign, 8, Convert.ToSByte, (n, b) => (sbyte)-Convert.ToSByte(n, b), I1));
                UInt8Octal = doc.CreateParser(CreateIntegerParser(octal, sign, 8, Convert.ToByte, (n, b) => (byte)-Convert.ToByte(n, b), U1));
                SInt16Octal = doc.CreateParser(CreateIntegerParser(octal, sign, 8, Convert.ToInt16, (n, b) => (short)-Convert.ToInt16(n, b), I2));
                UInt16Octal = doc.CreateParser(CreateIntegerParser(octal, sign, 8, Convert.ToUInt16, (n, b) => (ushort)-Convert.ToUInt16(n, b), U2));
                SInt32Octal = doc.CreateParser(OneOf(
                    Try(CreateIntegerParser(octal, sign, 8, Convert.ToInt32, (n, b) => -Convert.ToInt32(n, b), I4)),
                    CreateIntegerParser(octal, sign, 8, Convert.ToInt32, (n, b) => -Convert.ToInt32(n, b), string.Empty)
                    ));
                UInt32Octal = doc.CreateParser(CreateIntegerParser(octal, sign, 8, Convert.ToUInt32, (n, b) => (uint)-Convert.ToUInt32(n, b), U4));
                SInt64Octal = doc.CreateParser(CreateIntegerParser(octal, sign, 8, Convert.ToInt64, (n, b) => -Convert.ToInt64(n, b), I8));
                UInt64Octal = doc.CreateParser(octal.Before(String(U8)).Select(s => Convert.ToUInt64(s, 8)).Select<Literal<ulong>>(n => new(n)));
                SIntNOctal = doc.CreateParser(CreateIntegerParser(octal, sign, 8, (n, b) => (nint)Convert.ToInt64(n, b), (n, b) => (nint)(-Convert.ToInt64(n, b)), IN));
                UIntNOctal = doc.CreateParser(octal.Before(String(UN)).Select(s => Convert.ToUInt64(s, 8)).Select<Literal<nuint>>(n => new((nuint)n)));
                IntegerOctal = OneOf(
                    Try(SInt8Octal.Select(v => v.With<Literal>())),
                    Try(UInt8Octal.Select(v => v.With<Literal>())),
                    Try(SInt16Octal.Select(v => v.With<Literal>())),
                    Try(UInt16Octal.Select(v => v.With<Literal>())),
                    Try(UInt32Octal.Select(v => v.With<Literal>())),
                    Try(SInt64Octal.Select(v => v.With<Literal>())),
                    Try(UInt64Octal.Select(v => v.With<Literal>())),
                    Try(SIntNOctal.Select(v => v.With<Literal>())),
                    Try(UIntNOctal.Select(v => v.With<Literal>())),
                    SInt32Octal.Select(v => v.With<Literal>())
                    );
            }

            Integer = OneOf(
                Try(IntegerHex),
                Try(IntegerOctal),
                IntegerDecimal
                );

            Parser<char, string> real =
                Map(string.Concat,
                    sign,
                    OneOf(
                        Try(Map((i, dot, f) => i + dot + f, dec, Char('.'), digit.ManyString())),
                        Try(Map((i, dot) => i + dot, dec, Char('.'))),
                        Map((dot, f) => dot + f, Char('.'), digit.ManyString())
                        )
                    );

            Float32 = doc.CreateParser(real.Before(String(R4).Optional()).Select(s => new Literal<float>(Convert.ToSingle(s))));
            Float64 = doc.CreateParser(real.Before(String(R8).Optional()).Select(s => new Literal<double>(Convert.ToDouble(s))));

            Real = OneOf(
                Try(Float64).Select(v => v.With<Literal>()),
                Float32.Select(v => v.With<Literal>())
                );

            Parser = OneOf(
                Try(Integer),
                Real
                );
        }
    }
}
