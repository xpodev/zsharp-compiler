using System.Collections.Generic;

namespace ZSharp.CG
{
    public class ModuleBuilder
        : NamedItem
    {
        private readonly GlobalScopeBuilder _globalScope;
        private readonly List<TypeBuilder> _types = new();

        protected internal readonly ModuleDefinition _def;

        public ModuleBuilder(string name) : base(name)
        {
            _def = ModuleDefinition.CreateModule(name, ModuleKind.Dll);
            _globalScope = new(_def.Types[0]);
        }

        public TypeBuilder DefineType(string name)
        {
            TypeBuilder builder = new(name, null);

            _types.Add(builder);
            _def.Types.Add(builder._def);

            return builder;
        }

        public FunctionBuilder DefineFunction(string name) => 
            _globalScope.DefineFunction(name);

        public FunctionBuilder DefineFunction(string name, TypeReference returnType) =>
            _globalScope.DefineFunction(name, returnType);

        public TypeReference GetTypeReference(System.Type type) => 
            type is null ? null : new(_def.ImportReference(type), GetTypeReference(type.DeclaringType));

        public void Write(string path) => _def.Write(path);
    }
}
