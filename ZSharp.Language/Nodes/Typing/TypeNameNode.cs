using System.Collections.Generic;
using System.Linq;

namespace ZSharp.Language
{
    public record class TypeNameNode(
        FullyQualifiedName FullName,
        IEnumerable<NodeInfo<ModifiedObject<TypeNode>>> TypeArguments
        ) : TypeNode
    {
        public override TypeNameNode AsTypeName()
        {
            return this;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(FullName);
            if (TypeArguments.Any())
            {
                sb.Append('<');
                sb.Append(string.Join(", ", TypeArguments.Select(node => node.Object)));
                sb.Append('>');
            }
            return sb.ToString();
        }

        public override Type GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
