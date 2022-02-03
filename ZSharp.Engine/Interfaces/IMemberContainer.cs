namespace ZSharp.Engine
{
    public interface IMemberContainer : INamedItem
    {
        INamedItem GetMember(string name);
    }
}
