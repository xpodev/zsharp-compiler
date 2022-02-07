using Pidgin;
using System;
using System.Collections.Generic;
using Xunit;

namespace ZSharp.Parser.Tests
{
    public class NumberLiterals
    {
        static NumberLiteral parser = new();

        private static void ExpectLiteral<T>(Parser<char, Core.Literal> parser, string s, T expected)
        {
            Core.Literal result = parser.ParseOrThrow(s);

            T actual = Assert.IsType<T>(result.Value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        //[InlineData("0.0", 0f)]
        //[InlineData("1.0", 1f)]
        //[InlineData("1.5", 1.5f)]
        //[InlineData("0.", 0f)]
        //[InlineData(".0", 0f)]
        //[InlineData(".5", .5f)]
        //[InlineData(".010", .010f)]
        //[InlineData("1.", 1f)]
        //[InlineData("190.010", 190.010f)]
        //[InlineData("190.", 190f)]
        [InlineData("0.0f32", 0f)]
        [InlineData("1.0f32", 1f)]
        [InlineData("1.5f32", 1.5f)]
        [InlineData("0.f32", 0f)]
        [InlineData(".0f32", 0f)]
        [InlineData(".5f32", .5f)]
        [InlineData(".010f32", .010f)]
        [InlineData("1.f32", 1f)]
        [InlineData("190.010f32", 190.010f)]
        [InlineData("190.f32", 190f)]
        public void ExpectFloat(string s, float f)
        {
            ExpectLiteral(parser.Real, s, f);
        }

        #region Integers

        static string GetBaseString(int @base) =>
            @base switch
            {
                8 => "0o",
                10 => string.Empty,
                16 => "0x",
                _ => throw new ArgumentException("Invalid base: " + @base, nameof(@base))
            };

        static string ToString(long x, int @base) =>
            (x < 0 ? "-" : "") + GetBaseString(@base) + Convert.ToString(Math.Abs(x), @base);

        static string ToSignedString(long x, int @base) =>
            (x < 0 ? "-" : "+") + GetBaseString(@base) + Convert.ToString(Math.Abs(x), @base);

        static string ToString(ulong x, int @base) => ToString((long)x, @base);

        static string ToSignedString(ulong x, int @base) => ToSignedString((long)x, @base);

        static void ExpectInt8(int @base)
        {
            for (sbyte i = -100; i <= 100; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.I1, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.I1, i);
            }
        }

        static void ExpectUInt8(int @base)
        {
            for (byte i = 0; i <= 200; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.U1, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.U1, i);
            }
        }

        static void ExpectInt16(int @base)
        {
            for (short i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.I2, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.I2, i);
            }
        }

        static void ExpectUInt16(int @base)
        {
            for (ushort i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.U2, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.U2, i);
            }
        }

        static void ExpectInt32(int @base)
        {
            for (int i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base), i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base), i);
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.I4, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.I4, i);
            }
        }

        static void ExpectUInt32(int @base)
        {
            for (uint i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.U4, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.U4, i);
            }
        }

        static void ExpectInt64(int @base)
        {
            for (long i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.I8, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.I8, i);
            }
        }

        static void ExpectUInt64(int @base)
        {
            for (ulong i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.U8, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.U8, i);
            }
        }

        static void ExpectNInt(int @base)
        {
            for (nint i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.IN, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.IN, i);
            }
        }

        static void ExpectNUInt(int @base)
        {
            for (nuint i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, ToString(i, @base) + NumberLiteral.UN, i);
                ExpectLiteral(parser.Integer, ToSignedString(i, @base) + NumberLiteral.UN, i);
            }
        }

        #endregion

        public abstract class Integers
        {
            protected abstract int Base { get; }

            [Fact]
            public void ExpectInt8HexaDecimal()
            {
                ExpectInt8(Base);
            }

            [Fact]
            public void ExpectUInt8HexaDecimal()
            {
                ExpectUInt8(Base);
            }

            [Fact]
            public void ExpectInt16HexaDecimal()
            {
                ExpectInt16(Base);
            }

            [Fact]
            public void ExpectUInt16HexaDecimal()
            {
                ExpectUInt16(Base);
            }

            [Fact]
            public void ExpectInt32HexaDecimal()
            {
                ExpectInt32(Base);
            }

            [Fact]
            public void ExpectUInt32HexaDecimal()
            {
                ExpectUInt32(Base);
            }

            [Fact]
            public void ExpectInt64HexaDecimal()
            {
                ExpectInt64(Base);
            }

            [Fact]
            public void ExpectUInt64HexaDecimal()
            {
                ExpectUInt64(Base);
            }

            [Fact]
            public void ExpectNIntHexaDecimal()
            {
                ExpectNInt(Base);
            }

            [Fact]
            public void ExpectNUIntHexaDecimal()
            {
                ExpectNUInt(Base);
            }
        }

        public class IntegersDecimals : Integers
        {
            protected override int Base => 10;
        }

        public class IntegersHexadecimals : Integers
        {
            protected override int Base => 16;
        }
    }
}
