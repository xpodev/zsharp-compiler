using System.Collections;
using System.Collections.Generic;

namespace ZSharp.Core
{
    public class Collection 
        : Expression
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
            return string.Join(", ", Items);
        }
    }
}
