namespace ZSharp.CG
{
    public class Property
        : NamedItem
        , IProperty
    {      
        public TypeReference Type { get; }

        public TypeReference DeclaringType { get; }

        /// <summary>
        /// The getter method for this property.
        /// </summary>
        public MethodReference Getter { get; }

        /// <summary>
        /// The setter method for this property.
        /// </summary>
        public MethodReference Setter { get; }

        IType ITypedItem.Type => Type;

        IType IMember.DeclaringType => DeclaringType;

        IMethod IProperty.Getter => Getter;

        IMethod IProperty.Setter => Setter;

        public Property(PropertyDefinition property)
            : base(property.Name)
        {
            Type = TypeReference.Resolve(property.PropertyType);
            DeclaringType = TypeReference.Resolve(property.DeclaringType);
            Getter = new(property.GetMethod, DeclaringType);
            Setter = new(property.SetMethod, DeclaringType);
        }
    }
}
