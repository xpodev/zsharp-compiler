using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class SRFFunctionOverload
        : NamedItem
        , IInvocable
        , IDependencyProvider<SRFObject>
        , IEnumerable<IFunction>
        , INamedItemsContainer<IFunction>
        , INamedItemsContainer<SRFFunctionOverload>
    {
        public List<IFunction> Overloads { get; } = new();

        public SRFFunctionOverload(string name)
            : base(name)
        {

        }

        public SRFFunctionOverload(IFunction function)
            : this(function.Name)
        {
            Add(function);
        }

        public void Add(IFunction function) =>
            Overloads.Add(function);

        public void Add(SRFFunctionOverload functions) =>
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
                        func is MethodReference method &&
                        method.DeclaringType.SRF == type.SRF &&
                        func.GetParameterTypes().Select(t => t.SRF).SequenceEqual(
                            types.Skip(1).Select(t => t.SRF)
                        )
                    );
                if (candidates.Count == 1) return candidates[0];

                IType[] rest = types.Skip(1).ToArray();
                candidates = Overloads.FindAll(
                    func =>
                        func.IsInstance &&
                        func is MethodReference method &&
                        method.DeclaringType.SRF == type.SRF &&
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

        //public Expression Compile(GenericProcessor<IDependencyFinder> proc, Context ctx)
        //{
        //    DependencyFinder finder = proc as DependencyFinder;
        //    foreach (IFunction function in Overloads)
        //    {
        //        if (function is SRFObject dep) finder.DependencyGraph.AddDependency(this, dep);
        //    }
        //    return this;
        //}

        public IEnumerable<SRFObject> GetDependencies()
        {
            return Overloads
                .Where(item => item is Function func && !func.IsResolved)
                .Cast<SRFObject>();
        }

        public IEnumerable<SRFObject> GetDependencies(SRFObject dependant)
        {
            //if (dependant is not Function) return System.Array.Empty<SRFObject>();

            return GetDependencies();
        }

        public Result<string, Expression> Invoke(params object[] args)
        {
            IFunction function = Get(args.Select(arg => arg.GetType()).ToArray());
            if (function is null)
                return new($"Could not find overload for function {Name} with arguments: {string.Join(", ", args.Select(arg => arg.GetType().Name))}", null);
            return function.Invoke(args);
        }
    }
}
