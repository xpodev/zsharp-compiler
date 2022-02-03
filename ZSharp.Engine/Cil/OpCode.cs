using MSIL = System.Reflection.Emit;
using MCIL = Mono.Cecil.Cil;

namespace ZSharp.Engine.Cil
{
    public struct OpCode
    {
        public MSIL.OpCode MSIL { get; }

        public MCIL.OpCode MCIL { get; }

        public OpCode(MSIL.OpCode srf, MCIL.OpCode mc)
        {
            MSIL = srf;
            MCIL = mc;
        }
    }
}
