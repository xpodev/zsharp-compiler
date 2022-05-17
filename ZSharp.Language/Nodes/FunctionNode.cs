using System.Collections.Generic;

namespace ZSharp.Language
{
    public record class FunctionNode(
        NodeInfo<Identifier> Name, 
        NodeInfo<ModifiedObject<TypeNode>> Type,
        List<NodeInfo<Identifier>> TypeParameters,
        List<NodeInfo<ModifiedObject<TypedItemNode>>> Parameters,
        NodeInfo<FunctionBodyNode> Body
        ) : TypedItemNode(Name, Type)
    {
        public NodeInfo<ModifiedObject<TypeNode>> ReturnType
        {
            get => Type;
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

            foreach (var typeParameter in TypeParameters)
            {
                throw new System.NotImplementedException();
                //function.AddTypeParameter(typeParameter.GetCompilerObject(ctx));
            }

            foreach (var parameter in Parameters)
            {
                function.AddParameter(new(parameter.Object.Object.Object));
            }

            return function;
        }
    }
}
