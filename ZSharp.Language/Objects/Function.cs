using System.Collections.Generic;

namespace ZSharp.Language
{
    internal class Function : Node
    {
        public NodeInfo<Identifier> Name { get; set; }

        public List<NodeInfo<Identifier>> GenericParameters { get; set; }

        public List<NodeInfo<Identifier>> Parameters { get; set; }

        public NodeInfo<Expression> Type { get; set; }

        public NodeInfo<FunctionBody> Body { get; set; }

        internal Function Create()
        {
            return Body.Object.DeclaringFunction = this;
        }

        public override string ToString()
        {
            string generic = GenericParameters.Count > 0 ? $"<{string.Join(", ", GenericParameters)}>" : string.Empty;
            string parameters = $"({string.Join(" ", Parameters)})";
            return $"func {Name}{generic}{parameters}: {Type}";
        }
    }
}
