using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface ILanguageEngine
    {
        void AddAssemblyReference(string path);

        void AddAssemblyReference(System.Reflection.Assembly assembly);

        void AddDocument(DocumentInfo document);

        void Setup();

        void FinishCompilation(string path);

        IParser GetParser();

        IEnumerable<INodeProcessor> GetNodeProcessors();
        
        IEnumerable<IObjectProcessor> GetObjectProcessors();
    }
}
