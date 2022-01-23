namespace ZSharp.Engine
{
    public interface IGeneric<T>
        where T : IGeneric<T>
    {
        public T MakeGeneric(params IType[] types);
    }
}
