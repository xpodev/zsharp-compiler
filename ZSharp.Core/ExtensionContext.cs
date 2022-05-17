using System;
using System.Collections.Generic;

namespace ZSharp.Core
{
    public class ExtensionContext
    {
        private readonly Dictionary<Type, object> _singletons = new();

        public ExtensionContext AddSingleton<T>(T singleton) where T : class
        {
            if (!_singletons.TryAdd(typeof(T), singleton))
                throw new InvalidOperationException($"Singleton for {typeof(T).Name} already exists.");
            return this;
        }

        public T GetSingleton<T>() where T : class 
            => _singletons.TryGetValue(typeof(T), out object result) ? result as T : null;
    }
}
