using ZSharp.Core;

namespace ZSharp.Engine
{
    internal class ModifierProcessor : BaseProcessor
    {
        public ModifierProcessor(Engine engine) : base(engine)
        {
        }

        public override BuildResult<Error, Node> Process(Node node)
        {
            if (node is ModifiedObject modified)
            {
                //if (modified.Modifiers.Count == 0)
                //{
                //    return new(node);
                //}
                //else throw new NotImplementedException();
                return new(modified.Object);
            }
            return new(node);
        }
    }
}
