namespace ZSharp.CG
{
    public class MethodBuilder
        : FunctionBuilder
        , IMethodBuilder
    {
        public TypeBuilder DeclaringType { get; private set; }

        IType IMember.DeclaringType => DeclaringType;

        public bool IsStatic
        {
            get => _def.IsStatic;
            set => _def.IsStatic = value;
        }

        internal MethodBuilder(string name, TypeBuilder declaringType, TypeReference returnType) 
            : base(name, returnType)
        {
            DeclaringType = declaringType;

            // FunctionBuilder automatically sets IsStatic to true
            IsStatic = false;
        }
    }
}
