using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public class TypeReference 
        : NamedItem
        , IType
    {
        public System.Type SRF { get; }

        public Mono.Cecil.TypeReference MC { get; }

        public TypeReference(System.Type type) 
            : this(type, Resolve(type))
        {
            
        }

        public TypeReference(System.Type srf, Mono.Cecil.TypeReference mc) : base(srf.Name)
        {
            SRF = srf;
            MC = mc;

            //foreach (MethodInfo method in srf.GetMethods())
            //{
            //    AddItem(new FunctionOverload(new Function(method)));
            //}

            //foreach (System.Type type in srf.GetNestedTypes())
            //{
            //    AddItem(new Type(type));
            //}
        }

        public INamedItem GetMember(string name)
        {
            MemberInfo[] members = SRF.GetMember(name);
            if (members.Cast<MethodInfo>().All(o => o is not null))
            {
                FunctionOverload functions = new FunctionOverload(name);
                foreach (MemberInfo member in members) 
                    functions.Add(new FunctionReference(member as MethodInfo));
                return functions;
            }
            return null;
        }

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

        private static Mono.Cecil.TypeReference Resolve(System.Type type) =>
            Context.CurrentContext.Module.MC.ImportReference(type);
    }
}
