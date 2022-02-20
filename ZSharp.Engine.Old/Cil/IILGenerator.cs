using System.Collections.Generic;

namespace ZSharp.Engine.Cil
{
    public interface IILGenerator
    {
        IEnumerable<ILInstruction> GetIL();
    }
}
