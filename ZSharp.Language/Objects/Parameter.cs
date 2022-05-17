namespace ZSharp.Language
{
    public class Parameter : NodeWrapper<TypedItemNode>, IParameterBuilder
    {
        public string Name { get; set; }

        public IType Type { get; set; }

        public int Index { get; }

        public Parameter(TypedItemNode node, IType type, int index) : base(node)
        {
            Name = node.Name.Object.Name;

            Type = type;
            Index = index;
        }

        public void Build()
        {

        }
    }
}
