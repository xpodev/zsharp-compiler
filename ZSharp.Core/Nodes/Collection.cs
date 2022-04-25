using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZSharp.Core
{
    public record class Collection
        : Expression
        , IEnumerable<NodeInfo>
    {
        public List<NodeInfo> Items { get; set; }

        public static readonly Collection Empty = new();

        public int Count => Items.Count;

        public Collection(params NodeInfo[] expressions) : this((IEnumerable<NodeInfo>)expressions)
        {

        }

        public Collection(IEnumerable<NodeInfo> expressions)
        {
            Items = expressions is null ? new() : new(expressions);
        }

        public Collection(Collection original, params NodeInfo[] items)
            : this((IEnumerable<NodeInfo>)original)
        {
            Items.AddRange(items);
        }

        public void Add(NodeInfo expression) => Items.Add(expression);

        public void AddRange(IEnumerable<NodeInfo> source) => Items.AddRange(source);

        public void Clear() => Items.Clear();

        public IEnumerator<NodeInfo> GetEnumerator()
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

        public static NodeInfo Create(IEnumerable<NodeInfo> objects) =>
            objects.Any() ? new(objects.Select(o => o.FileInfo).Aggregate(FileInfo.Combine), new Collection(objects)) : new(new(), Empty);

        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }
    }

    public record class Collection<T>
        : Expression
        , IEnumerable<NodeInfo<T>>
        where T : Node
    {
        public List<NodeInfo<T>> Items { get; set; }

        public static readonly Collection<T> Empty = new();

        public Collection(params NodeInfo<T>[] expressions) : this((IEnumerable<NodeInfo<T>>)expressions)
        {

        }

        public Collection(IEnumerable<NodeInfo<T>> expressions)
        {
            Items = expressions is null ? new() : new(expressions);
        }

        public Collection(Collection<T> original, params NodeInfo<T>[] items)
            : this((IEnumerable<NodeInfo<T>>)original)
        {
            Items.AddRange(items);
        }

        public void Add(NodeInfo<T> expression) => Items.Add(expression);

        public void AddRange(IEnumerable<NodeInfo<T>> source) => Items.AddRange(source);

        public IEnumerator<NodeInfo<T>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            return $"[{string.Join(", ", Items)}]";
        }

        public static NodeInfo<Collection<T>> Create(IEnumerable<NodeInfo<T>> objects) =>
            objects.Any() ? new(objects.Select(o => o.FileInfo).Aggregate(FileInfo.Combine), new Collection<T>(objects)) : new(new(), Empty);

        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
