namespace ZSharp.Engine
{
    public class NodeModifier : INamedObject
    {
        protected string name;

        public string Name => name;

        public virtual Core.Node Modify(Core.Node @object)
        {
            return @object;
        }
    }
    
    public class NodeModifier<T> : NodeModifier
        where T : Core.Node
    {
        public sealed override Core.Node Modify(Core.Node @object)
        {
            return @object is T typedObject ? Modify(typedObject) : null;
        }

        public virtual Core.Node Modify(T @object)
        {
            return @object;
        }
    }
}
