using System.Collections.Generic;
using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public class ProjectScope
    {
        private readonly Stack<SearchScope> _scopes = new();

        public SearchScope GlobalScope { get; }

        public SearchScope CurrentScope => _scopes.Peek();

        public ProjectScope()
        {
            GlobalScope = EnterScope();
        }

        public void AddItem(INamedItem item, string name = null) =>
            AddItem<INamedItem>(item, name);

        public void AddItem<T>(T item, string name = null) where T : class, INamedItem
        {
            GlobalScope.AddItem(item, name);
        }

        public void AddLocalItem(INamedItem item, string name = null) =>
            AddLocalItem<INamedItem>(item, name);

        public void AddLocalItem<T>(T item, string name = null) where T : class, INamedItem
        {
            CurrentScope.AddItem(item, name);
        }

        public SearchScope EnterScope()
        {
            SearchScope scope = new();
            InsertScope(scope);
            return scope;
        }

        public void InsertScope(SearchScope scope) => _scopes.Push(scope);

        public void ExitScope()
        {
            _scopes.Pop();
        }

        public T GetItem<T>(string name) where T : class, INamedItem
        {
            foreach (SearchScope scope in _scopes)
            {
                if (scope.TryGetItem(name, out T result)) return result;
            }
            return null;
        }

        public NamedItem GetItem(string name) => GetItem<NamedItem>(name);

        public SearchScope GetOrCreateNamespace(string ns) =>
            GetOrCreateNamespace(ns?.Split('.'));

        public SearchScope GetOrCreateNamespace(params string[] parts)
        {
            SearchScope ns = GlobalScope;
            if (parts is null) return ns;
            foreach (string part in parts)
            {
                if (ns.TryGetItem<Namespace>(part) is Namespace nested)
                {
                    ns = nested;
                }
                else ns.AddItem(ns = new Namespace(part));
            }
            return ns;
        }
    }
}
