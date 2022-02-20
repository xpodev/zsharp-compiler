using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public class FunctionReference
        : NamedItem
        , IFunction
    {
        private readonly IParameter[] _parameters;

        public MethodInfo? SRF { get; }

        public Mono.Cecil.MethodReference? MC { get; }

        public IEnumerable<IParameter> Parameters => _parameters;

        public bool IsStatic => SRF.IsStatic;

        public bool IsInstance => !IsStatic;

        public bool IsVirtual => SRF.IsVirtual;

        public FunctionReference(MethodInfo method)
            : this(method, Resolve(method))
        {

        }

        public FunctionReference(MethodInfo srf, Mono.Cecil.MethodReference mc)
            : base(srf.Name)
        {
            SRF = srf;
            MC = mc;

            ParameterInfo[] srfParameters = SRF.GetParameters();
            _parameters = new ParameterReference[srfParameters.Length];
            for (int i = 0; i < _parameters.Length; i++)
            {
                _parameters[i] = new ParameterReference(
                    this, 
                    new TypeReference(srfParameters[i].ParameterType, null),
                    i,
                    srfParameters[i].Name ?? "arg" + i
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

        public IFunction? MakeGeneric(params IType[] types)
        {
            if (types is null) return this;
            if (types.Length == 0) return this;

            if (!SRF.IsGenericMethod) 
                throw new System.Exception($"{Name}({string.Join(", ", (IEnumerable<object>)_parameters)}) is not a generic method");

            System.Type[] srfTypes = new System.Type[types.Length];

            GenericInstanceMethod mc = new(MC);
            for (int i = 0; i < types.Length; i++)
            {
                mc.GenericArguments.Add(types[i].MC);
                srfTypes[i] = types[i].SRF;
            }

            return new FunctionReference(SRF.MakeGenericMethod(srfTypes), mc);
        }

        public IEnumerable<IType>? GetParameterTypes() =>
            SRF?.GetParameters()?.Select(p => new TypeReference(p.ParameterType));

        public BuildResult<ErrorType, Expression?> Invoke(params Expression[] args)
        {
            BuildResult<ErrorType, Expression?> result = new(null);

            if (SRF is null) return result.Error($"function {Name} is not callable (something is really messed up)");

            return 
                IsStatic
                ? 
                SRF.Invoke(null, args.ToArray()) is Expression expr
                ? result.Return<Expression?>(expr)
                : result.Error($"{Name}({string.Join(", ", (IEnumerable<object>)args)}) did not return a valid compile time value")
                : result.Error($"Could not invoke non-static method {Name}");
        }

        protected static Mono.Cecil.MethodReference Resolve(MethodInfo method) =>
            Context.CurrentContext.Module.MC.ImportReference(method);
    }
}
