using System.Collections.Generic;
using System.Linq;

namespace ZSharp.CG
{
    public class Assembly
        : NamedItem
        , IAssembly
    {
        protected internal AssemblyDefinition _def;

        private readonly List<IType> _definedTypes = new(), _exportedTypes = new();

        public IEnumerable<IType> GetDefinedTypes()
        {
            return _definedTypes;
        }

        public IEnumerable<IType> GetForwardedTypes()
        {
            return _exportedTypes;
        }

        public Assembly(AssemblyDefinition assembly) : base(assembly.Name.Name)
        {
            _def = assembly;
            
        }

        public Assembly(string name) 
            : this(AssemblyDefinition.CreateAssembly(new(name, new()), name, default(ModuleKind)))
        {
            foreach (ModuleDefinition module in _def.Modules)
            {
                _definedTypes.AddRange(module.Types.Select(TypeReference.Resolve));
            }

            foreach (ModuleDefinition module in _def.Modules)
            {
                _exportedTypes.AddRange(module.ExportedTypes.Select(type => type.Resolve()).Select(TypeReference.Resolve));
            }
        }
    }
}
