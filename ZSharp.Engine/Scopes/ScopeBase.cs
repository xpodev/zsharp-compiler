using System.Collections.Generic;

namespace ZSharp.Engine
{
    public class ScopeBase : IContext
    {
        private readonly Dictionary<string, INamedObject> _items = new();

        private readonly List<string> _importedScopes = new();

        public IReadOnlyList<string> ImportedScopes => _importedScopes.AsReadOnly();

        public INamedObject GetObject(string name)
        {
            return _items.TryGetValue(name, out INamedObject result) ? result : null;
        }

        public T GetObject<T>(string name) 
            where T : class, INamedObject
            => (this as IContext).GetObject<T>(name);

        public bool TryAddObject(INamedObject @object, string alias = null)
        {
            return _items.TryAdd(alias ?? @object.Name, @object);
        }

        /// <summary>
        /// Imports all items from the given named scope into this scope.
        /// </summary>
        /// <typeparam name="T">The type of the imported scope.</typeparam>
        /// <param name="scope">The scope to import.</param>
        /// <returns>A list of errors for all the items that couldn't be imported.</returns>
        public List<ErrorType> ImportScope<T>(T scope) 
            where T : ScopeBase, INamedObject
        {
            List<ErrorType> errors = new();

            _importedScopes.Add(scope.Name);
            foreach (INamedObject item in scope._items.Values)
            {
                if (item is Namespace) continue;

                if (!TryAddObject(item))
                    errors.Add(new("Duplicate name: " + item.Name));
            }

            return errors;
        }
    }
}
