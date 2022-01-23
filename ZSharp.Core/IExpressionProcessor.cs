namespace ZSharp.Core
{
    public interface IExpressionProcessor<T>
        where T : class
    {
        void PreProcess();

        Result<T, ObjectInfo> Process(Result<T, ObjectInfo> expression);

        void PostProcess();
    }
}
