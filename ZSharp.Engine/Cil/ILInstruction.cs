namespace ZSharp.Engine.Cil
{
    public class ILInstruction 
        : Core.Expression
    {
        internal MSILInstruction SRF => new(OpCode.SRF, Operand);

        internal MCILInstruction MC => new(OpCode.MC, Operand);

        public OpCode OpCode { get; }

        public object Operand { get; }

        public ILInstruction(OpCode opCode, object operand = null)
        {
            OpCode = opCode;
            Operand = operand;
        }
    }
}
