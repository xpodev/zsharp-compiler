using System;

namespace ZSharp.SR
{
    public class TypeBuilder
        : TypeReference
        , ITypeBuilder
    {
        protected internal TypeReference EmptyType = new(typeof(void), null);

        protected internal readonly RE.TypeBuilder _def;

        public new TypeBuilder DeclaringType { get; }
        
        IType IMember.DeclaringType => throw new NotImplementedException();

        public TypeBuilder(string name, TypeBuilder owner) : this(owner._def.DefineNestedType(name), owner)
        {

        }

        internal TypeBuilder(RE.TypeBuilder def, TypeBuilder owner)
            : base(def, owner)
        {
            _def = def;
            DeclaringType = owner;
        }

        public void Build()
        {

        }

        public FieldBuilder DefineField(string name) => DefineField(name, EmptyType);

        public FieldBuilder DefineField(string name, TypeReference type)
        {
            FieldBuilder builder = new(name, this, type);

            return builder;
        }

        public IMethodBuilder DefineMethod(string name)
        {
            throw new NotImplementedException();
        }

        public TypeBuilder DefineType(string name)
        {
            TypeBuilder builder = new(name, this);

            return builder;
        }

        IFieldBuilder ITypeBuilder.DefineField(string name) => DefineField(name);

        ITypeBuilder ITypeBuilder.DefineType(string name) => DefineType(name);
    }
}
