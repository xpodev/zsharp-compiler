using MSIL = System.Reflection.Emit;
using MCIL = Mono.Cecil.Cil;

namespace ZSharp.Engine.Cil
{
    public struct OpCode
    {
        public MSIL.OpCode SRF { get; }

        public MCIL.OpCode MC { get; }

        public OpCode(MSIL.OpCode srf, MCIL.OpCode mc)
        {
            SRF = srf;
            MC = mc;
        }
    }
}
