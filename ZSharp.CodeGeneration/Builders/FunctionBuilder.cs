using System;
using System.Collections.Generic;
using System.Linq;

namespace ZSharp.CG
{
    public class FunctionBuilder
        : NamedItem
        , IFunctionBuilder
    {
        private readonly List<ParameterBuilder> _parameters = new();
        private readonly ReturnParameterBuilder _returnParameter;

        private TypeReference _returnType;

        protected internal readonly MethodDefinition _def;

        public IEnumerable<IType> Input => _parameters.Select(p => p.Type);

        public ReturnParameterBuilder ReturnParameter => _returnParameter;

        public TypeReference ReturnType
        {
            get => _returnType;
            set => _returnParameter.Type = _returnType = value;
        }

        IType ITypedItem.Type => null;

        IParameterBuilder IFunctionBuilder.ReturnParameter => _returnParameter;

        public FunctionBuilder(string name, TypeReference returnType) : base(name)
        {
            _def = new(name, MethodAttributes.CompilerControlled, returnType._ref);
            _returnType = returnType;
            _returnParameter = new(_def.MethodReturnType, returnType);

            _def.IsStatic = true;
        }

        public void Build() { }

        public ParameterBuilder DefineParameter(string name, IType type)
        {
            if (type is not TypeReference _ref)
                throw new ArgumentException($"Argument must be instance of {nameof(TypeReference)}", nameof(type));

            ParameterBuilder builder = new(name, _ref);

            _parameters.Add(builder);
            _def.Parameters.Add(builder._def);

            return builder;
        }

        IParameterBuilder IFunctionBuilder.DefineParameter(string name, IType type) => 
            DefineParameter(name, type);
    }
}
