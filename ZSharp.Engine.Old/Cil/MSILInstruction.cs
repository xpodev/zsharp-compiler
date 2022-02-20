using MSIL = System.Reflection.Emit;

namespace ZSharp.Engine.Cil
{
    internal class MSILInstruction
    {
        public MSIL.OpCode OpCode { get; }

        public object Operand { get; }

        public MSILInstruction(MSIL.OpCode opCode, object operand = null)
        {
            OpCode = opCode;
            Operand = operand;
        }

        public void EmitTo(MSIL.ILGenerator il)
        {
            switch (Operand)
            {
                case IFunction function:
                    il.Emit(OpCode, function.SRF);
                    break;
                case IType type:
                    il.Emit(OpCode, type.SRF);
                    break;
                case IParameterBuilder parameter:
                    il.Emit(OpCode, parameter.Position);
                    break;
                case float @float:
                    il.Emit(OpCode, @float);
                    break;
                case double @double:
                    il.Emit(OpCode, @double);
                    break;
                case int @int:
                    il.Emit(OpCode, @int);
                    break;
                case short @short:
                    il.Emit(OpCode, @short);
                    break;
                case string @string:
                    il.Emit(OpCode, @string);
                    break;
                case null:
                    il.Emit(OpCode);
                    break;
                default:
                    throw new System.ArgumentException($"Invalid type", nameof(Operand));
            }
        }
    }
}
