namespace ZSharp.Core
{
    public interface IExpressionProcessor
    {
        void PreProcess();

        ObjectInfo Process(ObjectInfo expression);

        void PostProcess();
    }
}
