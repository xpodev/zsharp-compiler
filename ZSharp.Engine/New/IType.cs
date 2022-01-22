namespace ZSharp.Engine
{
    public interface IType : INamedItem
    {
        public System.Type SRF { get; }

        public Mono.Cecil.TypeReference MC { get; }

        public INamedItem GetMember(string name);

        public IType MakeGeneric(params IType[] types);
    }
}
