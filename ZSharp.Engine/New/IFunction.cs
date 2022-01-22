using System.Collections.Generic;

namespace ZSharp.Engine
{
    public interface IFunction : INamedItem
    {
        public System.Reflection.MethodInfo SRF { get; }

        public Mono.Cecil.MethodReference MC { get; }

        //public IEnumerable<IParameter> Parameters { get; }

        public bool IsStatic { get; }

        public bool IsInstance { get; }

        public IType DeclaringType { get; }

        IFunction MakeGeneric(params IType[] types);

        bool IsCallableWith(params IType[] types);

        IEnumerable<IType> GetParameterTypes();
    }
}
