using Mono.Cecil;
using System.Reflection.Emit;

namespace ZSharp.Engine
{
    public class SRFModule
    {
        public readonly ModuleBuilder SRF;
        public readonly ModuleDefinition MC;

        public readonly TypeDefinition MCGlobalScope;

        public SRFModule(ModuleBuilder srf, ModuleDefinition mc)
        {
            SRF = srf;
            MC = mc;
            MCGlobalScope = MC.Types[0];
        }
    }
}
