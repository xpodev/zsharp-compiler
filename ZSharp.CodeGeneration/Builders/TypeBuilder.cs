using System.Collections.Generic;

namespace ZSharp.CG
{
    public class TypeBuilder
        : TypeReference
        , ITypeBuilder
    {
        internal static readonly TypeBuilder EmptyType = new("<Empty>", null);

        protected readonly Dictionary<string, IMember> _members = new();

        protected internal readonly TypeDefinition _def;

        public new TypeBuilder DeclaringType { get; }

        public new IReadOnlyList<FieldBuilder> Fields { get; } = new List<FieldBuilder>();

        public new IReadOnlyList<TypeReference> Interfaces { get; } = new List<TypeReference>();

        public new IReadOnlyList<MethodBuilder> Methods { get; } = new List<MethodBuilder>();

        public new IReadOnlyList<PropertyBuilder> Properties { get; } = new List<PropertyBuilder>();

        public new IReadOnlyList<TypeBuilder> NestedTypes { get; } = new List<TypeBuilder>();

        IReadOnlyList<IFieldBuilder> ITypeBuilder.Fields => Fields;

        IReadOnlyList<IType> IType.Interfaces => Interfaces;

        IReadOnlyList<IMethodBuilder> ITypeBuilder.Methods => Methods;

        IReadOnlyList<IPropertyBuilder> ITypeBuilder.Properties => Properties;

        IReadOnlyList<ITypeBuilder> ITypeBuilder.NestedTypes => NestedTypes;

        public TypeBuilder(string name, TypeBuilder owner)
            : this(new TypeDefinition(null, name, TypeAttributes.Class), owner)
        {

        }

        internal TypeBuilder(TypeDefinition def, TypeBuilder owner) : base(def, owner)
        {
            _def = def;

            DeclaringType = owner;
        }

        public void Build() { }

        public FieldBuilder DefineField(string name) => DefineField(name, EmptyType);

        public FieldBuilder DefineField(string name, TypeReference fieldType)
        {
            FieldBuilder builder = new(name, this, fieldType);

            // todo: check result;
            AddMember(builder);
            _def.Fields.Add(builder._def);

            return builder;
        }

        public PropertyBuilder DefineProperty(string name) => DefineProperty(name, EmptyType);

        public PropertyBuilder DefineProperty(string name, TypeReference propertyType)
        {
            PropertyBuilder builder = new(name, this, propertyType);

            // todo: check result.
            AddMember(builder);
            _def.Properties.Add(builder._def);

            return builder;
        }

        public MethodBuilder DefineMethod(string name)
        {
            MethodBuilder builder = new(name, this, EmptyType);

            // todo: check result.
            AddMember(builder);
            _def.Methods.Add(builder._def);

            return builder;
        }

        public MethodBuilder DefineMethod(string name, TypeReference returnType)
        {
            MethodBuilder builder = new(name, this, returnType);

            // todo: check result.
            AddMember(builder);
            _def.Methods.Add(builder._def);

            return builder;
        }

        public TypeBuilder DefineType(string name)
        {
            TypeBuilder builder = new(name, this);

            // todo: check result.
            AddMember(builder);
            _def.NestedTypes.Add(builder._def);

            return builder;
        }

        IFieldBuilder ITypeBuilder.DefineField(string name) => DefineField(name);

        IMethodBuilder ITypeBuilder.DefineMethod(string name) => DefineMethod(name);

        IPropertyBuilder ITypeBuilder.DefineProperty(string name) => DefineProperty(name);

        ITypeBuilder ITypeBuilder.DefineType(string name) => DefineType(name);

        protected bool AddMember(IMember item)
        {
            return _members.TryAdd(item.Name, item);
        }
    }
}
