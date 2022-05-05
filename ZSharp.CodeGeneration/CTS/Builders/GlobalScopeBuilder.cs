namespace ZSharp.CG
{
    public class GlobalScopeBuilder
        : TypeBuilder
    {
        internal GlobalScopeBuilder(TypeDefinition def) : base(def, null)
        {

        }

        public FunctionBuilder DefineFunction(string name)
            => DefineFunction(name, EmptyType);

        public FunctionBuilder DefineFunction(string name, TypeReference returnType)
        {
            MethodBuilder builder = new(name, this, returnType);

            // todo: check result.
            AddMember(builder);
            _def.Methods.Add(builder._def);

            return builder;
        }
    }
}
