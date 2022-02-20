//using Mono.Cecil;

//namespace ZSharp.Engine
//{
//    public class ParameterBuilder
//        : NamedItem
//        , IParameterBuilder
//    {
//        public FunctionBuilder DeclaringFunction { get; }

//        public short Position { get; }

//        public IType Type { get; }

//        public ParameterBuilder SRF { get; }

//        public ParameterDefinition MC { get; }

//        IFunction IParameter.DeclaringFunction => DeclaringFunction;

//        public ParameterBuilder(
//            FunctionBuilder owner, 
//            string name, 
//            int position,
//            IType type
//            ) 
//            : base(name)
//        {
//            DeclaringFunction = owner;
//            Position = (short)position;
//            Type = type;

//            MC = new(name, ParameterAttributes.None, type.MC);
//            SRF = owner.SRF.DefineParameter();
//        } 
//    }
//}