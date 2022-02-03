//using Mono.Cecil;
//using System;
//using System.Reflection;
//using System.Reflection.Emit;
//using ZSharp.Core;

//namespace ZSharp.Engine.New
//{
//    public class Parameter
//        : NamedItem
//        , IParameter
//        , IBuilder
//        //, IResolve
//    {
//        public int Index { get; }

//        public ParameterBuilder SRF { get; private set; }

//        public ParameterDefinition MC { get; private set; }

//        public Function DeclaringFunction { get; }

//        Mono.Cecil.ParameterReference IParameter.MC => MC;

//        public IType Type => throw new NotImplementedException();

//        public Parameter(Function func, string name, int index) : base(name)
//        {
//            DeclaringFunction = func;
//            Index = index;
//        }

//        public Expression Compile(GenericProcessor<IBuilder> proc, Context ctx)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
