using System.Linq;

namespace ZSharp.Parser
{
    public class CodeBlockParser
    {
        public Parser<char, CodeBlock> Block { get; }

        public Parser<char, NodeInfo<CodeBlock>> Parser { get; }

        public CodeBlockParser(DocumentParser doc)
        {
            Parser<char, string> block = null;
            block = OneOf(
                Map(
                (l, c, r) => string.Concat(l, c, r),
                Char('{'),
                Rec(() => block),
                Char('}')
                ),
                AnyCharExcept('{', '}').Select(char.ToString)
            ).ManyString();
            Block = block.Between(Char('{'), Char('}')).Select(s => new CodeBlock(s));
            Parser = doc.CreateParser(Block);
        }
    }
}
