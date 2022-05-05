namespace ZSharp.CG
{
    public class PropertyBuilder
        : NamedItem
        , IPropertyBuilder
    {
        internal protected readonly PropertyDefinition _def;

        private MethodBuilder _getter, _setter;
        
        public TypeBuilder DeclaringType { get; }

        public TypeReference Type { get; }

        public MethodBuilder Getter
        {
            get => _getter;
            set {
                if (value is null) _getter = null;
                else if (value.Parameters.Count != 1)
                    throw new System.ArgumentException("Getter must have exactly 1 parameter.", nameof(value));
                else if (value.Parameters[0].Type != DeclaringType)
                    throw new System.ArgumentException("Setter must be in the same type as the property.", nameof(value));
                else if (value.ReturnType != Type)
                    throw new System.ArgumentException("Getter must return same type as the property.", nameof(value));
                else _getter = value;
                _def.GetMethod = _getter?._def;
            }
        }

        public MethodBuilder Setter
        {
            get => _setter;
            set
            {
                if (value is null) _setter = null;
                else if (value.Parameters.Count != 2)
                    throw new System.ArgumentException("Setter must have exactly 2 parameters", nameof(value));
                else if (value.Parameters[0].Type != DeclaringType)
                    throw new System.ArgumentException("Setter must be in the same type as the property.", nameof(value));
                else if (value.Parameters[1].Type != Type)
                    throw new System.ArgumentException("Setter second parameter type must be the same as the property type.", nameof(value));
                else _setter = value;
                _def.SetMethod = _setter?._def;
            }
        }

        IType IMember.DeclaringType => DeclaringType;

        IType ITypedItem.Type => Type;

        IMethodBuilder IPropertyBuilder.Getter
        {
            get => Getter;
            set => Getter = 
                value as MethodBuilder ?? 
                throw new System.ArgumentException($"Must be an instance of {nameof(MethodBuilder)}", nameof(value));
        }

        IMethodBuilder IPropertyBuilder.Setter
        {
            get => Setter;
            set => Setter =
                value as MethodBuilder ??
                throw new System.ArgumentException($"Must be an instance of {nameof(MethodBuilder)}", nameof(value));                
        }

        internal PropertyBuilder(string name, TypeBuilder owner, TypeReference type) 
            : base(name)
        {
            _def = new PropertyDefinition(name, PropertyAttributes.None, type._ref);

            DeclaringType = owner;
            Type = type;
        }
        
        public void Build() { }
    }
}