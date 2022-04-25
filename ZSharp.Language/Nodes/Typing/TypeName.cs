using System.Collections.Generic;
using System.Linq;

namespace ZSharp.Language
{
    public record class TypeName(
        FullyQualifiedName FullName,
        IEnumerable<NodeInfo<Type>> TypeArguments
        ) : Type
    {
        public override TypeName AsTypeName()
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

        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
