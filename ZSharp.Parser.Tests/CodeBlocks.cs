using ZSharp.Core;
using Xunit;
using Pidgin;

namespace ZSharp.Parser.Tests
{
    public class CodeBlocks
    {
        private static CodeBlockParser parser = DocumentParser.Instance.CodeBlock;

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("this is a code block")]
        [InlineData(" this is a code block")]
        [InlineData("this is a code block ")]
        [InlineData(" this is a code block ")]
        [InlineData("{}")]
        [InlineData("{ }")]
        [InlineData("{ this is }")]
        [InlineData("{ this is { a nested } }")]
        public void ExpectCodeBlock(string source)
        {
            CodeBlock block = parser.Block.ParseOrThrow('{' + source + '}');

            Assert.Equal(source, block.Source);
        }
    }
}
