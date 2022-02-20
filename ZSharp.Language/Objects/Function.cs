using System.Collections.Generic;

namespace ZSharp.Language
{
    internal class Function : DocumentObject
    {
        public ObjectInfo<Identifier> Name { get; set; }

        public List<ObjectInfo<Identifier>> GenericParameters { get; set; }

        public List<ObjectInfo<Identifier>> Parameters { get; set; }

        public ObjectInfo<Expression> Type { get; set; }

        public ObjectInfo<FunctionBody> Body { get; set; }

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
