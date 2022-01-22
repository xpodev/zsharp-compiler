using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZSharp.Engine
{
    public class FunctionOverload
        : NamedItem
        , IEnumerable<IFunction>
        , INamedItemsContainer<IFunction>
        , INamedItemsContainer<FunctionOverload>
    {
        public List<IFunction> Overloads { get; } = new();

        public FunctionOverload(string name)
            : base(name)
        {

        }

        public FunctionOverload(IFunction function)
            : this(function.Name)
        {
            Add(function);
        }

        public void Add(IFunction function) =>
            Overloads.Add(function);

        public void Add(FunctionOverload functions) =>
            Overloads.AddRange(functions.Overloads);

        public IFunction Get(params System.Type[] types) =>
            Get(types.Select(type => new TypeReference(type)).ToArray());

        public IFunction Get(params IType[] types)
        {
            List<IFunction> candidates;

            // instance methods
            if (types.FirstOrDefault() is IType type)
            {
                candidates = Overloads.FindAll(
                    func => 
                        func.IsInstance && 
                        func.DeclaringType.SRF == type.SRF &&
                        func.GetParameterTypes().Select(t => t.SRF).SequenceEqual(
                            types.Skip(1).Select(t => t.SRF)
                        )
                    );
                if (candidates.Count == 1) return candidates[0];

                IType[] rest = types.Skip(1).ToArray();
                candidates = Overloads.FindAll(
                    func =>
                        func.IsInstance &&
                        func.DeclaringType.SRF == type.SRF &&
                        func.IsCallableWith(rest)
                    );

                if (candidates.Count == 1) return candidates[0];
            }

            candidates = Overloads.FindAll(
                func => 
                    func.IsStatic &&
                    func.GetParameterTypes().Select(t => t.SRF)
                    .SequenceEqual(types.Select(t => t.SRF))
                );

            if (candidates.Count == 1) return candidates[0];

            candidates = Overloads.FindAll(
                func => func.IsStatic && func.IsCallableWith(types)
                );
            if (candidates.Count == 0) return null;
                //throw new System.Exception(
                //    $"Could not find a suitable overload for method '{Name}' " +
                //    $"with types ({string.Join(", ", types.Select(type => type.Name))})"
                //    );
            return candidates[0];
        }

        public IEnumerator<IFunction> GetEnumerator()
        {
            return Overloads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Overloads.GetEnumerator();
        }
    }
}
