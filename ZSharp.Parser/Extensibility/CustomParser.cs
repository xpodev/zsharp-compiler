namespace ZSharp.Parser.Extensibility
{
    public abstract class CustomParser<T> : ICustomParser<T>
        where T : DocumentObject
    {
        private Parser<char, T> _parser;

        public string Name { get; }

        public string Namespace { get; }

        public virtual Parser<char, T> Parser
        {
            get => _parser;
            set
            {
                _parser = value;
                ObjectParser = _parser.Cast<DocumentObject>();
            }
        }

        public Parser<char, DocumentObject> ObjectParser { get; private set; }

        Parser<char, DocumentObject> ICustomParser.Parser
        {
            get => ObjectParser;
            set => ObjectParser = value;
        }

        protected CustomParser(string name, string @namespace)
        {
            Name = name;
            Namespace = @namespace;
        }

        public T Parse(string input) => Parser.ParseOrThrow(input);

        public T Parse(System.IO.TextReader input) => Parser.ParseOrThrow(input);
    }
}
