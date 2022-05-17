namespace ZSharp.Parser.Extensibility
{
    public interface ILanguageExtension
    {
        void Initialize(Parser parser, ExtensionContext ctx);
    }
}
