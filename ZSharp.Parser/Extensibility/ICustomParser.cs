namespace ZSharp.Parser.Extensibility
{
    public interface ICustomParser
    {
        string Name { get; }

        Parser<char, Node> Parser { get; }
    }

    public interface ICustomParser<out T> : ICustomParser
        where T : Node
    {
        //new Parser<char, T> Parser { get; set; }

        Parser<char, Node> ObjectParser { get; }
    }
}