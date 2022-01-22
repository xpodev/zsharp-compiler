using Mono.Cecil;
using ZSharp.Core;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;

namespace ZSharp.Engine
{
    public class Class 
        : SearchScope
        , IType
        , IBuildable
        , ICompilable
        , IOnAddCallback<Namespace>
        , IOnAddCallback<Class>
        , IModifierProvider<string>
        , INamedItemsContainer<Class>
        //, INamedItemsContainer<Function>
        //, INamedItemsContainer<Property>
        //, INamedItemsContainer<Field>
        //, INamedItemsContainer<Event>
    {
        public TypeDefinition MC { get; private set; }

        public TypeBuilder SRF { get; private set; }

        System.Type IType.SRF => SRF;

        Mono.Cecil.TypeReference IType.MC => MC;

        public Namespace Namespace { get; set; }

        private readonly List<string> _modifiers = new();

        public IType Base { get; }

        public List<IType> Interfaces { get; }

        public Class DeclaringClass { get; private set; }

        public bool IsSealed { get; set; }

        public bool IsAbstract { get; set; }

        public Class(string name, IType @base, List<IType> interfaces) : base(name)
        {
            Base = @base ?? Context.CurrentContext.TypeSystem.Object;
            Interfaces = interfaces;
        }

        public void OnAdd(Namespace parent)
        {
            Namespace = parent;
        }

        public void OnAdd(Class parent)
        {
            DeclaringClass = parent;
        }

        public void Add(NamedItem item)
        {
            if (item is Class cls) cls.DeclaringClass = this;
            //if (item is FunctionBuilder func) func.DeclaringType = this;
            AddItem(item);
        }

        public void Add(Class cls)
        {
            AddItem(cls);
        }

        public Expression Compile(GenericProcessor<ICompilable> proc, Context context)
        {
            foreach (NamedItem item in this)
            {
                if (item is ICompilable compilable) compilable.Compile(proc, context);
            }

            return this;
        }

        public void AddModifier(string modifier)
        {
            if (HasModifier(modifier))
                throw new System.InvalidOperationException($"Duplicate modifier: {modifier}");
            _modifiers.Add(modifier);
        }

        public bool HasModifier(string modifier) => _modifiers.Contains(modifier);

        public INamedItem GetMember(string name) => GetItem(name);

        public IType MakeGeneric(params IType[] types)
        {
            if (types is null) return this;
            if (types.Length == 0) return this;

            System.Type[] srfTypes = new System.Type[types.Length];

            GenericInstanceType mc = new(MC);
            for (int i = 0; i < types.Length; i++)
            {
                mc.GenericArguments.Add(types[i].MC);
                srfTypes[i] = types[i].SRF;
            }

            return new TypeReference(SRF.MakeGenericType(srfTypes), mc);
        }

        public Expression Compile(GenericProcessor<IBuildable> proc, Context ctx)
        {
            // SRF type definition
            {
                System.Reflection.TypeAttributes srfAttribute = System.Reflection.TypeAttributes.Class;

                if (IsSealed) srfAttribute |= System.Reflection.TypeAttributes.Sealed;
                if (IsAbstract) srfAttribute |= System.Reflection.TypeAttributes.Abstract;

                System.Type[] interfaces =
                    Interfaces?.Select(i => i.SRF)?.ToArray() ?? System.Array.Empty<System.Type>();

                if (DeclaringClass is not null)
                {
                    srfAttribute |= System.Reflection.TypeAttributes.NestedPrivate;

                    SRF = DeclaringClass.SRF.DefineNestedType(
                        Name,
                        srfAttribute,
                        Base.SRF,
                        interfaces
                        );
                }
                else
                {
                    string fullName = Namespace?.FullName;
                    if (fullName is not null) fullName += "." + Name;
                    else fullName = Name;
                    SRF = Context.CurrentContext.Module.SRF.DefineType(
                        fullName,
                        srfAttribute,
                        Base.SRF,
                        interfaces
                        );
                }
            }

            // MC type definition
            {
                Mono.Cecil.TypeAttributes mcAttributes = Mono.Cecil.TypeAttributes.Class;

                if (IsSealed) mcAttributes |= Mono.Cecil.TypeAttributes.Sealed;
                if (IsAbstract) mcAttributes |= Mono.Cecil.TypeAttributes.Abstract;

                MC = new(
                    (Namespace ?? DeclaringClass?.Namespace)?.FullName,
                    Name,
                    mcAttributes,
                    Base.MC
                    );

                if (DeclaringClass is null)
                    ctx.Module.MC.Types.Add(MC);
                else
                {
                    DeclaringClass.MC.NestedTypes.Add(MC);
                    MC.DeclaringType = DeclaringClass.MC;
                }
            }

            if (DeclaringClass is null)
                ctx.Scope.AddItem(this);

            foreach (NamedItem item in this)
            {
                if (item is IBuildable builder) builder.Compile(proc, ctx);
            }

            return this;
        }
    }
}
