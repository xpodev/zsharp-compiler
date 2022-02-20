using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZSharp.Core
{
    public class Collection
        : DocumentObject
        , IEnumerable<ObjectInfo>
    {
        public List<ObjectInfo> Items { get; set; }

        public static readonly Collection Empty = new();

        public int Count => Items.Count;

        public Collection(params ObjectInfo[] expressions) : this((IEnumerable<ObjectInfo>)expressions)
        {

        }

        public Collection(IEnumerable<ObjectInfo> expressions)
        {
            Items = expressions is null ? new() : new(expressions);
        }

        public Collection(Collection original, params ObjectInfo[] items)
            : this((IEnumerable<ObjectInfo>)original)
        {
            Items.AddRange(items);
        }

        public void Add(ObjectInfo expression) => Items.Add(expression);

        public void AddRange(IEnumerable<ObjectInfo> source) => Items.AddRange(source);

        public void Clear() => Items.Clear();

        public IEnumerator<ObjectInfo> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", Items)}]";
        }

        public static ObjectInfo Create(IEnumerable<ObjectInfo> objects) =>
            objects.Any() ? new(objects.Select(o => o.FileInfo).Aggregate(FileInfo.Combine), new Collection(objects)) : new(new(), Empty);
    }

    public class Collection<T>
        : DocumentObject
        , IEnumerable<ObjectInfo<T>>
        where T : DocumentObject
    {
        public List<ObjectInfo<T>> Items { get; set; }

        public static readonly Collection<T> Empty = new();

        public Collection(params ObjectInfo<T>[] expressions) : this((IEnumerable<ObjectInfo<T>>)expressions)
        {

        }

        public Collection(IEnumerable<ObjectInfo<T>> expressions)
        {
            Items = expressions is null ? new() : new(expressions);
        }

        public Collection(Collection<T> original, params ObjectInfo<T>[] items)
            : this((IEnumerable<ObjectInfo<T>>)original)
        {
            Items.AddRange(items);
        }

        public void Add(ObjectInfo<T> expression) => Items.Add(expression);

        public void AddRange(IEnumerable<ObjectInfo<T>> source) => Items.AddRange(source);

        public IEnumerator<ObjectInfo<T>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            return $"[{string.Join(", ", Items)}]";
        }

        public static ObjectInfo<Collection<T>> Create(IEnumerable<ObjectInfo<T>> objects) =>
            objects.Any() ? new(objects.Select(o => o.FileInfo).Aggregate(FileInfo.Combine), new Collection<T>(objects)) : new(new(), Empty);
    }
}
