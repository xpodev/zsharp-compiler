namespace ZSharp.Parser.Extensibility
{
    internal class ExtensionParser : ICustomParser
    {
        public ICustomParser Original { get; }

        public string Name => Original.Name;

        Parser<char, Node> ICustomParser.Parser
        {
            get => Original.Parser;
            set => Original.Parser = value;
        }

        public Parser<char, NodeInfo> Parser { get; internal set; }

        public ExtensionParser(ICustomParser original)
        {
            Original = original;
            Parser = original.Parser.WithObjectInfo().UpCast();
        }
    }
}
