using System.Collections.Generic;

namespace ZSharp.Core
{
    public interface ILanguageEngine //: System.Collections.Generic.IEnumerable<IExpressionProcessor>
    {
        void AddAssemblyReference(string path);

        void AddAssemblyReference(System.Reflection.Assembly assembly);

        void Setup();

        void FinishCompilation(string path);

        IParser GetParser();

        IEnumerable<INodeProcessor> GetProcessors();
    }
}
