using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public class Namespace 
        : SearchScope
        , IBuildable
        , ICompilable
        , INamedItemsContainer<Namespace>
        //, INamedItemsContainer<Class>
        , INamedItemsContainer<FunctionOverload>
    {
        public Namespace DeclaringNamespace { get; private set; } = null;

        public string FullName => DeclaringNamespace is null ? Name : DeclaringNamespace.FullName + "." + Name;

        public Namespace(string name) : base(name)
        {

        }

        public void Add(INamedItem item)
        {
            AddItem(item);

            if (item is IOnAddCallback<Namespace> onAddCallback) onAddCallback.OnAdd(this);
        }

        public void Add(Namespace item)
        {
            Add(item as NamedItem);
            item.DeclaringNamespace = this;
        }

        public void Add(FunctionOverload item)
        {
            Add(item as NamedItem);
        }

        //public void Add(Class cls)
        //{
        //    if (cls.Namespace is not null) throw new System.InvalidOperationException();
        //    cls.Namespace = this;
        //    Add(cls as NamedItem);
        //}

        public BuildResult<ErrorType, Expression> Compile(GenericProcessor<IBuildable> proc, Context ctx)
        {
            foreach (NamedItem item in this)
            {
                if (item is IBuildable builder) builder.Compile(proc, ctx);
            }
            return null;
        }

        public BuildResult<ErrorType, Expression> Compile(GenericProcessor<ICompilable> proc, Context ctx)
        {
            foreach (NamedItem item in this)
            {
                if (item is ICompilable builder) builder.Compile(proc, ctx);
            }

            return null;
        }
    }
}
