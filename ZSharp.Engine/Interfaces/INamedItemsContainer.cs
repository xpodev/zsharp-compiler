namespace ZSharp.Engine
{
    public interface INamedItemsContainer<in T> where T : INamedItem
    {
        public void Add(T item);
    }
}
