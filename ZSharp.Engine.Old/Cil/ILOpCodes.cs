using ZSharp.OldCore;
using ZSharp.Engine;

[assembly: CustomKeyword("ldarg", 100, OperatorFixity.Prefix)]
[assembly: CustomKeyword("call", 100, OperatorFixity.Prefix)]
[assembly: CustomKeyword("ldstr", 100, OperatorFixity.Prefix)]
[assembly: CustomKeyword("ldc_i4", 100, OperatorFixity.Prefix)]
[assembly: CustomKeywordLiteral("ldarg0")]
[assembly: CustomKeywordLiteral("ret")]

namespace ZSharp.Engine.Cil
{
    public static class ILOpCodes
    {
        public static ILInstruction LoadArgument(IParameterBuilder parameter) =>
            new(OpCodes.Ldarg, parameter);

        [KeywordOverload("ldarg")]
        public static ILInstruction LoadArgumentOptimized(IParameterBuilder parameter) =>
            parameter.Position switch
            {
                0 => new(OpCodes.Ldarg_0),
                1 => new(OpCodes.Ldarg_1),
                2 => new(OpCodes.Ldarg_2),
                3 => new(OpCodes.Ldarg_3),
                short x when byte.MinValue <= x && x <= byte.MaxValue => 
                    new(OpCodes.Ldarg_S, (byte)x),
                _ => LoadArgument(parameter)
            };

        //public static ILInstruction LoadField(IField field) { }

        public static ILInstruction LoadArgument(short index) =>
            new(OpCodes.Ldarg, index);

        public static ILInstruction LoadString(string s) =>
            new(OpCodes.Ldstr, s);

        public static ILInstruction LoadFloat32(float f) =>
            new(OpCodes.Ldc_R4, f);

        public static ILInstruction LoadFloat64(double d) =>
            new(OpCodes.Ldc_R8, d);

        public static ILInstruction LoadInt32(int value) =>
            new(OpCodes.Ldc_I4, value);

        public static ILInstruction LoadInt32Optimized(int value) =>
            value switch
            {
                0 => new(OpCodes.Ldc_I4_0),
                1 => new(OpCodes.Ldc_I4_1),
                2 => new(OpCodes.Ldc_I4_2),
                3 => new(OpCodes.Ldc_I4_3),
                4 => new(OpCodes.Ldc_I4_4),
                5 => new(OpCodes.Ldc_I4_5),
                6 => new(OpCodes.Ldc_I4_6),
                7 => new(OpCodes.Ldc_I4_7),
                8 => new(OpCodes.Ldc_I4_8),
                _ when sbyte.MinValue <= value && value <= sbyte.MaxValue => new(OpCodes.Ldc_I4_S, (sbyte)value),
                _ => LoadInt32(value)
            };

        public static ILInstruction LoadInt64(long value) =>
            new(OpCodes.Ldc_I8, value);

        public static ILInstruction NewObject(IType type)
        {
            return new(OpCodes.Newobj, type);
        }

        [KeywordOverload("call")]
        public static ILInstruction CallFunction(Expression function)
        {
            return CallFunction((Expression)Context.CurrentContext.Evaluate(function) as IFunction);
        }

        [KeywordOverload("ldc_i4")]
        public static ILInstruction LoadInt32(Literal literal)
        {
            if (literal.Value is not int value)
                throw new System.Exception("LDC_I4 can only be used with integers");
            return LoadInt32(value);
        }

        [KeywordOverload("ldarg")]
        public static ILInstruction LoadArgument(Literal literal)
        {
            if (literal.Value is not short value)
                throw new System.Exception("LDARG must be used with an int");
            return LoadArgument(value);
        }

        [KeywordOverload("ldarg0")]
        public static ILInstruction LoadArgument0()
        {
            return new(OpCodes.Ldarg_0);
        }

        [KeywordOverload("call")]
        public static ILInstruction CallFunction(IFunction function)
        {
            return new(OpCodes.Call, function);
        }

        [KeywordOverload("ret")]
        public static ILInstruction Return()
        {
            return new(OpCodes.Ret);
        }

        [KeywordOverload("ldstr")]
        public static ILInstruction LoadString(Literal literal)
        {
            if (literal.Value is not string s)
                throw new System.Exception($"LDSTR must be used with a string");
            return LoadString(s);
        }
    }
}
