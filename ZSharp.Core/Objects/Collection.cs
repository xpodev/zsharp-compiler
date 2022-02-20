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
            new(objects.Select(o => o.FileInfo).Aggregate(FileInfo.Combine), new Collection(objects));
    }

    public class Collection<T> : Collection
        where T : DocumentObject
    {
        public new List<ObjectInfo<T>> Items { get; set; }

        public static new readonly Collection<T> Empty = new();

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

        public new IEnumerator<ObjectInfo<T>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(", ", Items);
        }

        public static ObjectInfo<Collection<T>> Create(IEnumerable<ObjectInfo<T>> objects) =>
            objects.Count() == 0 ? new(new(), Empty) : new(objects.Select(o => o.FileInfo).Aggregate(FileInfo.Combine), new Collection<T>(objects));
    }
}
