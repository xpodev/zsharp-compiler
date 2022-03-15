using Pidgin;
using System;
using Xunit;

namespace ZSharp.Parser.Tests
{
    public class NumberLiterals
    {
        private static readonly NumberLiteral parser = DocumentParser.Instance.Literal.Number;

        private static void ExpectLiteral<T>(Parser<char, Core.NodeInfo<Core.Literal>> parser, string s, T expected)
        {
            Core.NodeInfo<Core.Literal> result = parser.ParseOrThrow(s);

            T actual = Assert.IsType<T>(result.Object.Value);

            Assert.Equal(expected, actual);
        }

        [Theory]
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

        [Theory]
        [InlineData("0.0", 0)]
        [InlineData("1.0", 1)]
        [InlineData("1.5", 1.5)]
        [InlineData("0.", 0)]
        [InlineData(".0", 0)]
        [InlineData(".5", .5)]
        [InlineData(".010", .010)]
        [InlineData("1.", 1)]
        [InlineData("190.010", 190.010)]
        [InlineData("190.", 190)]
        [InlineData("0.0f64", 0)]
        [InlineData("1.0f64", 1)]
        [InlineData("1.5f64", 1.5)]
        [InlineData("0.f64", 0)]
        [InlineData(".0f64", 0)]
        [InlineData(".5f64", .5)]
        [InlineData(".010f64", .010)]
        [InlineData("1.f64", 1)]
        [InlineData("190.010f64", 190.010)]
        [InlineData("190.f64", 190)]
        public void ExpectDouble(string s, double f)
        {
            ExpectLiteral(parser.Real, s, f);
        }


        public abstract class Integers
        {
            protected abstract int Base { get; }

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

            [Fact]
            public void ExpectInt8()
            {
                for (sbyte i = -100; i <= 100; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.I1, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.I1, i);
                }
            }

            [Fact]
            public void ExpectUInt8()
            {
                for (byte i = 0; i <= 200; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.U1, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.U1, i);
                }
            }

            [Fact]
            public void ExpectInt16()
            {
                for (short i = -1000; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.I2, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.I2, i);
                }
            }

            [Fact]
            public void ExpectUInt16()
            {
                for (ushort i = 0; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.U2, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.U2, i);
                }
            }

            [Fact]
            public void ExpectInt32()
            {
                for (int i = -1000; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base), i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base), i);
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.I4, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.I4, i);
                }
            }

            [Fact]
            public void ExpectUInt32()
            {
                for (uint i = 0; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.U4, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.U4, i);
                }
            }

            [Fact]
            public void ExpectInt64()
            {
                for (long i = -1000; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.I8, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.I8, i);
                }
            }

            [Fact]
            public void ExpectUInt64()
            {
                for (ulong i = 0; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.U8, i);
                }
            }

            [Fact]
            public void ExpectNInt()
            {
                for (nint i = -1000; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.IN, i);
                    ExpectLiteral(parser.Integer, ToSignedString(i, Base) + NumberLiteral.IN, i);
                }
            }

            [Fact]
            public void ExpectNUInt()
            {
                for (nuint i = 0; i <= 1000; i++)
                {
                    ExpectLiteral(parser.Integer, ToString(i, Base) + NumberLiteral.UN, i);
                }
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

        public class IntegersOctals : Integers
        {
            protected override int Base => 8;
        }
    }
}
