namespace ZSharp.Core
{
    public interface ILanguageEngine
    {
        void AddAssemblyReference(string path);

        void AddAssemblyReference(System.Reflection.Assembly assembly);

        void Setup();

        void BeginDocument(DocumentInfo document);

        void EndDocument();

        void FinishCompilation(string path);

        IExpressionProcessor NextProcessor();
    }
}
