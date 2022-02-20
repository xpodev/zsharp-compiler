using System;
using System.Collections;
using System.Collections.Generic;

namespace ZSharp.Engine
{
    public class GenericTypeOverload
        : NamedItem
        , IType
        , IEnumerable<IType>
        , INamedItemsContainer<IType>
        , INamedItemsContainer<GenericTypeOverload>
    {
        private readonly Dictionary<int, IType> Overloads = new();

        public GenericTypeOverload(string name) : base(name)
        {
        }

        public GenericTypeOverload(IType type) : this(type.Name)
        {
            Add(type);
        }

        public IType NonGenericType => Overloads[0];

        public System.Type SRF => NonGenericType.SRF;

        public Mono.Cecil.TypeReference MC => NonGenericType.MC;

        public void Add(IType item)
        {
            int numGenericParamaters = item.SRF.GetGenericArguments().Length;
            if (Overloads.ContainsKey(numGenericParamaters))
                throw new InvalidOperationException($"Generic overload for {Name} with {numGenericParamaters} already exists");
            Overloads[numGenericParamaters] = item;
        }

        public void Add(GenericTypeOverload items)
        {
            foreach (IType item in items)
            {
                Add(item);
            }
        }

        public IType Get(params IType[] types)
        {
            return Overloads[types.Length].MakeGeneric(types);
        }

        public IEnumerator<IType> GetEnumerator()
        {
            return Overloads.Values.GetEnumerator();
        }

        public INamedItem GetMember(string name)
        {
            return NonGenericType.GetMember(name);
        }

        public IType MakeGeneric(params IType[] types)
        {
            return NonGenericType.MakeGeneric(types);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Overloads.Values.GetEnumerator();
        }
    }
}
