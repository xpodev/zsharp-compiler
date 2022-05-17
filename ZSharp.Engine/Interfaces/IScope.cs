namespace ZSharp.Engine
{
    public interface IScope
    {
        /// <summary>
        /// Find and returns an object by its name from this context.
        /// </summary>
        /// <param name="name">The name of the object to search for.</param>
        /// <returns>The object with the given name, if found, otherwise <see cref="null"/>.</returns>
        public INamedObject GetObject(string name);

        /// <summary>
        /// Find and returns an object by its name from this context and cast it to the given type.
        /// </summary>
        /// <typeparam name="T">The desired output type.</typeparam>
        /// <param name="name">The name of the object to search for.</param>
        /// <returns>The object casted to type <typeparamref name="T"/> or <see cref="null"/> if could not cast or not found.</returns>
        public T GetObject<T>(string name) 
            where T : class, INamedObject
        {
            return GetObject(name) as T;
        }

        /// <summary>
        /// Adds an object to this context.
        /// </summary>
        /// <param name="object">The object to add.</param>
        /// <param name="alias">An alias to the object (object will not be added with original name).</param>
        /// <returns><see cref="true"/> if the object was added otherwise <see cref="false"/>.</returns>
        public bool TryAddObject(INamedObject @object, string alias = null);
    }
}
