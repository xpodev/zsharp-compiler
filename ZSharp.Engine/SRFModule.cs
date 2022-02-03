using Mono.Cecil;
using System.Reflection.Emit;

namespace ZSharp.Engine
{
    public class SRFModule
    {
        private int _globalScopeTypes = 0;

        public readonly ModuleBuilder SRF;
        public readonly ModuleDefinition MC;

        public readonly TypeDefinition MCGlobalScope;

        public SRFModule(ModuleBuilder srf, ModuleDefinition mc)
        {
            SRF = srf;
            MC = mc;
            MCGlobalScope = MC.Types[0];
        }

        public TypeReference DefineGlobalScopeType()
        {
            // todo: change this to a type builder
            return new(SRF.DefineType($"<Global>__{_globalScopeTypes++}"), MCGlobalScope);
        }
    }
}
