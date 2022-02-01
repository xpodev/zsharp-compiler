namespace ZSharp.Engine.Cil
{
    public class ILInstruction 
        : Core.Expression
    {
        internal MSILInstruction MSIL => new(OpCode.MSIL, Operand);

        internal MCILInstruction MCIL => new(OpCode.MCIL, Operand);

        public OpCode OpCode { get; }

        public object Operand { get; }

        public ILInstruction(OpCode opCode, object operand = null)
        {
            OpCode = opCode;
            Operand = operand;
        }
    }
}
