//using Mono.Cecil;
//using System;
//using System.Collections.Generic;
//using System.Reflection.Emit;

//namespace ZSharp.Engine
//{
//    public class FunctionBuilder
//        : NamedItem
//        , IFunction
//    {
//        private readonly List<ParameterBuilder> _parameters = new();

//        public Type DeclaringType { get; internal set; }

//        public IType ReturnType { get; set; }

//        public IReadOnlyList<IParameterBuilder> Parameters => _parameters.AsReadOnly();

//        public MethodDefinition MC { get; }

//        public MethodBuilder SRF { get; }

//        internal FunctionBuilder(string name, IType returnType = null)
//            : base(name)
//        {
//            ReturnType = returnType;
//        }

//        public ParameterBuilder AddParameter(string name, IType type = null)
//        {
//            ParameterBuilder parameter = new(this, name, _parameters.Count, type);
//            _parameters.Add(parameter);
//            return parameter;
//        }

//        public Function Build(Context ctx)
//        {
            
//        }

//        public void ResolveReferences(Context ctx)
//        {
//            if (ReturnType is null)
//            {
//                // todo: infer return type from body
//                ReturnType = ctx.TypeSystem.Void;
//            }


//        }
//    }
//}
