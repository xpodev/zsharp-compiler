using System.Collections.Generic;

namespace ZSharp.Engine.Cil
{
    public class ILBuilder
    {
        private readonly List<ILInstruction> _instructions;

        public List<ILInstruction> Instructions => _instructions;

        public int Length => _instructions.Count;

        public Function Function { get; }

        public ILBuilder(Function func, List<ILInstruction> instructions)
        {
            Function = func;
            _instructions = instructions;
        }

        public void Append(ILInstruction instruction) => 
            _instructions.Add(instruction);

        public void AppendMany(IEnumerable<ILInstruction> instructions) =>
            _instructions.AddRange(instructions);

        public void Emit(OpCode opCode, object operand = null) =>
            Append(new(opCode, operand));

        public void Build()
        {
            System.Reflection.Emit.ILGenerator srf = Function.SRF.GetILGenerator();
            Mono.Cecil.Cil.ILProcessor mc = Function.MC.Body.GetILProcessor();
            foreach (ILInstruction il in _instructions)
            {
                il.SRF.EmitTo(srf);
                il.MC.EmitTo(mc);
            }
        }
    }
}
