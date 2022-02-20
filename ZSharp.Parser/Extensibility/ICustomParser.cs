namespace ZSharp.Parser.Extensibility
{
    public interface ICustomParser
    {
        string Name { get; }

        Parser<char, DocumentObject> Parser { get; set; }
    }

    public interface ICustomParser<out T> : ICustomParser
        where T : DocumentObject
    {
        //new Parser<char, T> Parser { get; set; }

        Parser<char, DocumentObject> ObjectParser { get; }
    }
}