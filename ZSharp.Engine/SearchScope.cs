using System;
using System.Collections;
using System.Collections.Generic;

namespace ZSharp.Engine
{
    public class SearchScope 
        : NamedItem
        , IMemberContainer
        , IEnumerable<INamedItem>
    {
        private readonly Dictionary<string, INamedItem> _items = new();

        public SearchScope(string name = "") : base(name)
        {

        }

        public bool Contains(string name) => _items.ContainsKey(name);

        public INamedItem GetItem(string name) => _items[name];

        public INamedItem TryGetItem(string name) => 
            _items.TryGetValue(name, out INamedItem result) ? result : null;

        public INamedItem GetMember(string name) => GetItem(name);

        public bool TryGetItem(string name, out INamedItem value) =>
            _items.TryGetValue(name, out value);

        public T GetItem<T>(string name) where T : class, INamedItem =>
            GetItem(name) as T;

        public T TryGetItem<T>(string name) where T : class, INamedItem =>
            TryGetItem(name) as T;

        public bool TryGetItem<T>(string name, out T value) where T : class, INamedItem =>
            (value = TryGetItem<T>(name)) is not null;

        public void AddItem<T>(T item, string name = null) where T : class, INamedItem
        {
            name ??= item.Name;
            if (!Contains(name))
            {
                _items.Add(name, item);
                return;
            }

            if (GetItem(name) is INamedItemsContainer<T> container)
            {
                container.Add(item);
            }
            else throw new InvalidOperationException(
              $"This container already contains an object with the same name: {name}"
              );
        }

        public IEnumerator<INamedItem> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
