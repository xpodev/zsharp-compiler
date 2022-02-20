using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public class NamespaceDefinition 
        : NamedItem
        //, ICompilable
    {
        private readonly Namespace _ns;

        public NamespaceDefinition(Namespace ns) : base(ns.Name)
        {
            _ns = ns;
        }

        //public Expression Compile(GenericProcessor<ICompilable> proc, Context ctx)
        //{
        //    foreach (INamedItem item in _ns)
        //    {
        //        if (item is ICompilable compilable) compilable.Compile(proc, ctx);
        //    }

        //    return this;
        //}

        [KeywordOverload("namespace")]
        public static NamespaceDefinition CreateNamespace(Identifier id)
        {
            Namespace ns = Context.CurrentContext.Scope.GetItem<Namespace>(id.Name);
            if (ns is null)
            {
                ns = new Namespace(id.Name);
                Context.CurrentContext.Scope.AddItem(ns);
            }
            return new(ns);
        }

        [KeywordOverload("namespace")]
        public static NamespaceDefinition CreateNamespace(Namespace ns)
        {
            return new(ns);
        }

        [OperatorOverload(".")]
        public static NamespaceDefinition GetOrCreateNamespace(NamespaceDefinition current, Identifier id)
        {
            return new(current._ns.GetItem<Namespace>(id.Name));
        }
    
        [SurroundingOperatorOverload("{", "}")]
        public static Namespace Initialize(NamespaceDefinition ns, Collection items)
        {
            foreach (Expression item in items)
            {
                if ((Expression)Context.CurrentContext.Evaluate(item) is INamedItem named)
                {
                    ns._ns.Add(named);
                }
            }
            return ns._ns;
        }

        [SurroundingOperatorOverload("{", "}")]
        public static Namespace Initialize(NamespaceDefinition ns)
        {
            return ns._ns;
        }
    }
}
