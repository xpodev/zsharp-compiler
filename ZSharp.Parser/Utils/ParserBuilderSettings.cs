namespace ZSharp.Parser
{
    public struct ParserBuilderSettings
    {
        //public string Name { get; set; }

        public string Keyword { get; set; } = null;

        public bool IsModifiable { get; set; } = true;

        public bool AllowBlockDefinition { get; set; }

        public bool AllowDefault { get; set; }

        public BracketType BlockBracketType { get; set; } = BracketType.Curly;
    }
}
