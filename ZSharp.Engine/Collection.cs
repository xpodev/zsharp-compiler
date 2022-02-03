using System;
using System.Collections;
using System.Collections.Generic;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class CollectionOperators
    {
        [OperatorOverload(",")]
        public static ObjectInfo CreateCollection(ObjectInfo a, ObjectInfo b)
        {
            return new(FileInfo.Combine(a.FileInfo, b.FileInfo), a.Expression as Collection ?? new(a, b));
        }

        [OperatorOverload(",")]
        public static ECollection<Identifier> CreateCollection(Identifier a, Identifier b)
        {
            return new() { a, b };
        }

        [OperatorOverload(",")]
        public static ECollection<Identifier> CreaetCollection(ECollection<Identifier> collection, Identifier id)
        {
            collection.Add(id);
            return collection;
        }
    }

    public class ECollection<T>
        : Expression
        , ICollection<T>
        , IEnumerable<T>
        , IEnumerable
        , IList<T>
        , IReadOnlyCollection<T>
        , IReadOnlyList<T>
        , ICollection
        , IList
        where T : Expression
    {
        private readonly List<T> _list = new();

        public T this[int index] { get => ((IList<T>)_list)[index]; set => ((IList<T>)_list)[index] = value; }
        object? IList.this[int index] { get => ((IList)_list)[index]; set => ((IList)_list)[index] = value; }

        public int Count => ((ICollection<T>)_list).Count;

        public bool IsReadOnly => ((ICollection<T>)_list).IsReadOnly;

        public bool IsSynchronized => ((ICollection)_list).IsSynchronized;

        public object SyncRoot => ((ICollection)_list).SyncRoot;

        public bool IsFixedSize => ((IList)_list).IsFixedSize;

        public void Add(T item)
        {
            ((ICollection<T>)_list).Add(item);
        }

        public int Add(object? value)
        {
            return ((IList)_list).Add(value);
        }

        public void Clear()
        {
            ((ICollection<T>)_list).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)_list).Contains(item);
        }

        public bool Contains(object? value)
        {
            return ((IList)_list).Contains(value);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)_list).CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_list).CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_list).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)_list).IndexOf(item);
        }

        public int IndexOf(object? value)
        {
            return ((IList)_list).IndexOf(value);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)_list).Insert(index, item);
        }

        public void Insert(int index, object? value)
        {
            ((IList)_list).Insert(index, value);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)_list).Remove(item);
        }

        public void Remove(object? value)
        {
            ((IList)_list).Remove(value);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)_list).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }
    }

    public class ECollection : ECollection<Expression> { }
}
