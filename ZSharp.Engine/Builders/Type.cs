using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class Type
        //: SearchScope
        : SRFObject
        , IType
        , IBuildable
        , IDependencyFinder
        , IResolvable
    {
        private readonly ClassDeclaration _info;

        public Type DeclaringType { get; private set; }

        public IType Base { get; private set; }

        public TypeBuilder SRF { get; private set; }

        public TypeDefinition MC { get; private set; }

        public bool IsBuilt => !(SRF is null || MC is null);

        System.Type IType.SRF => SRF;

        Mono.Cecil.TypeReference IType.MC => MC;

        public Namespace Namespace { get; set; }

        public List<IType> Interfaces { get; } = new();

        public bool IsSealed { get; set; }

        public bool IsAbstract { get; set; }

        public Type(ClassDeclaration info) : base(info.Name)
        {
            _info = info;
        }

        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<IBuildable> proc, Context ctx)
        {
            if (IsBuilt) throw new InvalidOperationException($"Type {Name} is already built");

            BuildResult<ErrorType, Expression?> result = new(this);

            // SRF type definition
            {
                System.Reflection.TypeAttributes srfAttribute = System.Reflection.TypeAttributes.Class;

                if (IsSealed) srfAttribute |= System.Reflection.TypeAttributes.Sealed;
                if (IsAbstract) srfAttribute |= System.Reflection.TypeAttributes.Abstract;

                System.Type[] interfaces =
                    Interfaces?.Select(i => i.SRF)?.ToArray() ?? Array.Empty<System.Type>();

                if (DeclaringType is not null)
                {
                    srfAttribute |= System.Reflection.TypeAttributes.NestedPrivate;

                    SRF = DeclaringType.SRF.DefineNestedType(
                        Name,
                        srfAttribute
                        );
                }
                else
                {
                    string fullName = Namespace?.FullName;
                    if (fullName is not null) fullName += "." + Name;
                    else fullName = Name;
                    SRF = Context.CurrentContext.Module.SRF.DefineType(
                        fullName,
                        srfAttribute
                        );
                }
            }

            // MC type definition
            {
                Mono.Cecil.TypeAttributes mcAttributes = Mono.Cecil.TypeAttributes.Class;

                if (IsSealed) mcAttributes |= Mono.Cecil.TypeAttributes.Sealed;
                if (IsAbstract) mcAttributes |= Mono.Cecil.TypeAttributes.Abstract;

                MC = new(
                    (Namespace ?? DeclaringType?.Namespace)?.FullName,
                    Name,
                    mcAttributes
                    );

                if (DeclaringType is null)
                    ctx.Module.MC.Types.Add(MC);
                else
                {
                    DeclaringType.MC.NestedTypes.Add(MC);
                    MC.DeclaringType = DeclaringType.MC;
                }
            }

            if (DeclaringType is null)
                ctx.Scope.AddItem(this);

            //foreach (INamedItem item in this)
            //{
            //    if (item is IBuildable builder) builder.Compile(proc, ctx);
            //}

            return result;
        }

        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<IResolvable> proc, Context ctx)
        {
            BuildResult<ErrorType, Expression?> result = new(this);

            IType? @base;
            List<IType> interfaces = new();

            if (_info.Base is null) @base = ctx.TypeSystem!.Object;
            else if (_info.Base?.Expression is not Collection bases)
            {
                @base = proc.Process(_info.Base!.Expression).Value as IType;

                if (@base is null)
                    result.Error($"{Name}'s base class expression evaluated to null");
                else if (@base.SRF.IsInterface)
                {
                    interfaces.Add(@base);
                    @base = ctx.TypeSystem!.Object;
                }
            }
            else
            {
                BuildResult<ErrorType, ObjectInfo[]> basesResult = BuildResultUtils.CombineResults(bases.Select(ctx.Evaluate));
                List<IType> types = new(basesResult.Value.Cast<IType>());

                if (types.Count == 0)
                    @base = ctx.TypeSystem!.Object;
                else
                {
                    if (!types[0].SRF.IsInterface)
                    {
                        @base = types[0];
                        types.RemoveAt(0);
                    }
                    else
                    {
                        @base = ctx.TypeSystem!.Object;
                    }
                    interfaces.AddRange(types);
                }
            }

            if (@base is null)
                throw new Exception($"{Name}'s base class expression evaluated to null");

            Base = @base;
            Interfaces.AddRange(interfaces);

            {
                SRF.SetParent(@base.SRF);
                MC.BaseType = @base.MC;

                foreach (IType @interface in interfaces)
                {
                    SRF.AddInterfaceImplementation(@interface.SRF);
                    MC.Interfaces.Add(new InterfaceImplementation(@interface.MC));
                }
            }

            //foreach (INamedItem item in this)
            //{
            //    if (item is IResolvable resolve) resolve.Compile(proc, ctx);
            //}

            return result;
        }

        public BuildResult<ErrorType, Expression?> Compile(DependencyFinder finder, Context ctx)
        {
            if (_info.Base is not null)
                finder.FindDependencies(this, _info.Base);

            if (_info.MetaClass is not null)
                finder.FindDependencies(this, _info.MetaClass);

            return new(this);
        }

        public INamedItem GetMember(string name) => null; // GetItem(name);

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
    }
}
