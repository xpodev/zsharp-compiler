namespace ZSharp.Engine
{
    public interface IType 
        : INamedItem
        , IMemberContainer
        , IGeneric<IType>
    {
        public System.Type SRF { get; }

        public Mono.Cecil.TypeReference MC { get; }
    }
}
