using System.Collections.Generic;
using System.Linq;

namespace ZSharp.CG
{
    public class FunctionReference
        : NamedItem
        , IFunction
    {
        protected internal MC.MethodReference _ref;

        public TypeReference[] Input { get; }

        public ParameterReference[] Parameters { get; }

        public ReturnParameter ReturnParameter { get; }

        IReadOnlyList<IType> IFunction.Input => Input;

        IReadOnlyList<IParameter> IFunction.Parameters => Parameters;

        IParameter IFunction.ReturnParameter => ReturnParameter;

        IType ITypedItem.Type => null;

        public FunctionReference(MC.MethodReference method) : base(method.Name)
        {
            _ref = method;
            ReturnParameter = new(method.MethodReturnType);
            Parameters = method.Parameters.Select(p => new ParameterReference(p, this)).ToArray();
            Input = Parameters.Select(p => p.Type).ToArray();
        }
    }
}
