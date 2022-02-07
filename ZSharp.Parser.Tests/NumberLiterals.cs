using Pidgin;
using System;
using System.Collections.Generic;
using Xunit;

namespace ZSharp.Parser.Tests
{
    public class NumberLiterals
    {
        NumberLiteral parser = new();

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

        #region Integers.Decimal

        [Fact]
        public void ExpectInt8Decimal()
        {
            for (sbyte i = -100; i <= 100; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "i8", i);
            }
        }

        [Fact]
        public void ExpectUInt8Decimal()
        {
            for (byte i = 0; i <= 200; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "u8", i);
            }
        }

        [Fact]
        public void ExpectInt16Decimal()
        {
            for (short i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "i16", i);
            }
        }

        [Fact]
        public void ExpectUInt16Decimal()
        {
            for (ushort i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "u16", i);
            }
        }

        [Fact]
        public void ExpectInt32Decimal()
        {
            for (int i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString(), i);
                ExpectLiteral(parser.Integer, i.ToString() + "i32", i);
            }
        }

        [Fact]
        public void ExpectUInt32Decimal()
        {
            for (uint i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "u32", i);
            }
        }

        [Fact]
        public void ExpectInt64Decimal()
        {
            for (long i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "i64", i);
            }
        }

        [Fact]
        public void ExpectUInt64Decimal()
        {
            for (ulong i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "u64", i);
            }
        }

        [Fact]
        public void ExpectNIntDecimal()
        {
            for (nint i = -1000; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "I", i);
            }
        }

        [Fact]
        public void ExpectNUIntDecimal()
        {
            for (nuint i = 0; i <= 1000; i++)
            {
                ExpectLiteral(parser.Integer, i.ToString() + "U", i);
            }
        }

        #endregion
    }
}
