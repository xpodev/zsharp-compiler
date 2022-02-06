namespace ZSharp.CG
{
    public class MethodReference
        : FunctionReference
        , IMethod
    {
        public TypeReference DeclaringType { get; }

        public bool IsStatic => !_ref.HasThis;

        IType IMember.DeclaringType => DeclaringType;

        public MethodReference(MC.MethodReference method, TypeReference owner) : base(method)
        {
            DeclaringType = owner;
        }
    }
}
