namespace ZSharp.Cil
{
    public interface IInstruction
    {
        public OpCode OpCode { get; }

        public object Operand { get; set; }
    }
}
