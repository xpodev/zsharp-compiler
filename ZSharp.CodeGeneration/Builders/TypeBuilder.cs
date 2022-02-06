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

        public TypeBuilder(string name, TypeBuilder owner) 
            : this(new TypeDefinition(null, name, TypeAttributes.Class), owner)
        {

        }

        internal TypeBuilder(TypeDefinition def, TypeBuilder owner) : base(def, owner)
        {
            _def = def;
            DeclaringType = owner;
        }

        public FieldBuilder DefineField(string name) => DefineField(name, EmptyType);

        public FieldBuilder DefineField(string name, TypeReference fieldType)
        {
            FieldBuilder builder = new(name, this, fieldType);

            // todo: check result;
            AddMember(builder);
            _def.Fields.Add(builder._def);

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

        ITypeBuilder ITypeBuilder.DefineType(string name) => DefineType(name);

        protected bool AddMember(IMember item)
        {
            return _members.TryAdd(item.Name, item);
        }
    }
}
