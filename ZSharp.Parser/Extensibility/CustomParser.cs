namespace ZSharp.Parser.Extensibility
{
    public abstract class CustomParser<T> : ICustomParser<T>
        where T : Node
    {
        private Parser<char, T> _parser;

        public string Name { get; }

        public virtual Parser<char, T> Parser
        {
            get => _parser;
            set
            {
                _parser = value;
                ObjectParser = _parser.Cast<Node>();
            }
        }

        public Parser<char, Node> ObjectParser { get; private set; }

        Parser<char, Node> ICustomParser.Parser
        {
            get => ObjectParser;
            //set => ObjectParser = value;
        }

        protected CustomParser(string name)
        {
            Name = name;
        }

        public T Parse(string input) => Parser.ParseOrThrow(input);

        public T Parse(System.IO.TextReader input) => Parser.ParseOrThrow(input);
    }
}
