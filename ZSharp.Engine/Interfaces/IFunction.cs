using System.Collections.Generic;

namespace ZSharp.Engine
{
    public interface IFunction 
        : IInvocable
        , IGeneric<IFunction>
    {
        public System.Reflection.MethodInfo? SRF { get; }

        public Mono.Cecil.MethodReference? MC { get; }

        //public IEnumerable<IParameter> Parameters { get; }

        public bool IsStatic { get; }

        public bool IsInstance { get; }

        public bool IsVirtual { get; }

        bool IsCallableWith(params IType[] types);

        IEnumerable<IType>? GetParameterTypes();
    }
}
