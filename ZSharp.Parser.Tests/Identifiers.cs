using Pidgin;
using Xunit;

namespace ZSharp.Parser.Tests
{
    public class Identifiers
    {
        private static readonly IdentifierParser parser = DocumentParser.Instance.Identifier;

        [Theory]
        [InlineData("a")]
        [InlineData("_")]
        [InlineData("_a")]
        [InlineData("a_")]
        [InlineData("a0")]
        [InlineData("a1")]
        [InlineData("a2")]
        [InlineData("_0")]
        [InlineData("_1")]
        [InlineData("_00")]
        [InlineData("_01")]
        [InlineData("a'")]
        [InlineData("a''")]
        [InlineData("_''")]
        [InlineData("_0'0'0")]
        public void ExpectIdentifier(string source)
        {
            Core.NodeInfo<Core.Identifier> result = parser.Parser.ParseOrThrow(source);

            Assert.Equal(source, result.Object.Name);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("_")]
        [InlineData("_a")]
        [InlineData("a_")]
        [InlineData("a0")]
        [InlineData("a1")]
        [InlineData("a2")]
        [InlineData("_0")]
        [InlineData("_1")]
        [InlineData("_00")]
        [InlineData("_01")]
        [InlineData("a'")]
        [InlineData("a''")]
        [InlineData("_''")]
        [InlineData("_0'0'0")]
        public void ExpectIdentifierWithWhitespace(string source)
        {
            Core.NodeInfo<Core.Identifier> result = parser.Parser.ParseOrThrow(source + "     // asd\n/* \n \n ad */  \n");

            Assert.Equal(source, result.Object.Name);
        }
    }
}
