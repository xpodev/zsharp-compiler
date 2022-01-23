namespace ZSharp.Engine
{
    public interface IGenericCompilable<T>
        where T : IGenericCompilable<T>
    {
        string Compile(GenericProcessor<T> proc, Context ctx);
    }
}
