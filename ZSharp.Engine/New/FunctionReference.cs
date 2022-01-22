using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZSharp.Engine
{
    public class FunctionReference
        : NamedItem
        , IFunction
    {
        private readonly IParameter[] _parameters;

        public MethodInfo SRF { get; }

        public MethodReference MC { get; }

        public IEnumerable<IParameter> Parameters => _parameters;

        public bool IsStatic => SRF.IsStatic;

        public bool IsInstance => !IsStatic;

        public IType DeclaringType { get; }

        public FunctionReference(MethodInfo method)
            : this(method, Resolve(method))
        {

        }

        public FunctionReference(MethodInfo srf, MethodReference mc)
            : base(srf.Name)
        {
            SRF = srf;
            MC = mc;
            DeclaringType = new TypeReference(srf.DeclaringType);

            ParameterInfo[] srfParameters = SRF.GetParameters();
            _parameters = new ParameterReference[srfParameters.Length];
            for (int i = 0; i < _parameters.Length; i++)
            {
                _parameters[i] = new ParameterReference(
                    this, 
                    new TypeReference(srfParameters[i].ParameterType),
                    i,
                    srfParameters[i].Name
                    );
            }
        }

        public bool IsCallableWith(params IType[] types)
        {
            ParameterInfo[] parameters = SRF.GetParameters();
            if (types.Length != parameters.Length) return false;

            System.Type[] parameterTypes = parameters.Select(p => p.ParameterType).ToArray();
            for (int i = 0; i < parameterTypes.Length; i++)
            {
                if (!parameterTypes[i].IsAssignableFrom(types[i].SRF)) return false;
            }
            return true;
        }

        public IFunction MakeGeneric(params IType[] types)
        {
            if (types is null) return this;
            if (types.Length == 0) return this;

            System.Type[] srfTypes = new System.Type[types.Length];

            GenericInstanceMethod mc = new(MC);
            for (int i = 0; i < types.Length; i++)
            {
                mc.GenericArguments.Add(types[i].MC);
                srfTypes[i] = types[i].SRF;
            }

            return new FunctionReference(SRF.MakeGenericMethod(srfTypes), mc);
        }

        public IEnumerable<IType> GetParameterTypes() =>
            SRF.GetParameters().Select(p => new TypeReference(p.ParameterType));

        private static MethodReference Resolve(MethodInfo method) =>
            Context.CurrentContext.Module.MC.ImportReference(method);
    }
}
