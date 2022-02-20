using System.Reflection;

namespace ZSharp.Engine
{
    public class MethodReference
        : FunctionReference
        , IMethod
    {
        public IType DeclaringType { get; }

        public MethodReference(MethodInfo method) 
            : this(method, Resolve(method))
        {

        }

        public MethodReference(MethodInfo srf, Mono.Cecil.MethodReference mc) : base(srf, mc)
        {

        }
    }
}
