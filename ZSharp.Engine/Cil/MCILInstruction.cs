using Mono.Cecil.Cil;
using MCIL = Mono.Cecil.Cil;

namespace ZSharp.Engine.Cil
{
    internal class MCILInstruction
    {
        public MCIL.OpCode OpCode { get; }

        public object Operand { get; }

        public MCILInstruction(MCIL.OpCode opCode, object operand = null)
        {
            OpCode = opCode;
            Operand = operand;
        }

        public void EmitTo(ILProcessor il)
        {
            il.Append(GetInstruction());
        }

        public Instruction GetInstruction()
        {
            return Operand switch
            {
                IFunction function => Instruction.Create(OpCode, function.MC),
                IType type => Instruction.Create(OpCode, type.MC),
                IParameterBuilder parameter => Instruction.Create(OpCode, parameter.MC),
                double @double => Instruction.Create(OpCode, @double),
                float @float => Instruction.Create(OpCode, @float),
                int @int => Instruction.Create(OpCode, @int),
                short @short => Instruction.Create(OpCode, @short),
                string @string => Instruction.Create(OpCode, @string),
                _ => Instruction.Create(OpCode)
            };
        }
    }
}
