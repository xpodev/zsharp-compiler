using System.Collections.Generic;

namespace ZSharp.Language
{
    public record class FunctionNode : Node
    {       
        public NodeInfo<Identifier> Name { get; set; }

        public List<NodeInfo<Identifier>> TypeParameters { get; set; }

        public List<NodeInfo<TypedItemNode>> Parameters { get; set; }
        
        public NodeInfo<TypeNode> ReturnType { get; set; }

        public NodeInfo<FunctionBodyNode> Body { get; set; }

        internal FunctionNode Create()
        {
            return Body.Object.DeclaringFunction = this;
        }

        public override string ToString()
        {
            string generic = TypeParameters.Count > 0 ? $"<{string.Join(", ", TypeParameters)}>" : string.Empty;
            string parameters = $"({string.Join(" ", Parameters)})";
            return $"func {Name}{generic}{parameters}: {ReturnType}";
        }

        public override Function GetCompilerObject(IContext ctx)
        {
            Function function = new(this);

            foreach (var parameter in Parameters)
            {
                TypedItemNode node = parameter.Object;
                function.AddParameter(new(node, node.Type.Object.GetCompilerObject(ctx) as IType, function.Parameters.Count));
            }

            return function;
        }
    }
}
