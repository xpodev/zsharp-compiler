using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class ClassDeclaration 
        : NamedItem
        , IObjectDescriptor
    {
        public Expression MetaClass { get; private set; }

        public Expression Base { get; private set; }

        public List<Expression> Items { get; private set; }

        public ClassDeclaration(
            string name,
            Expression meta = null,
            Expression @base = null
            )
            : base(name)
        {
            MetaClass = meta;
            Base = @base;
        }

        [KeywordOverload("class")]
        public static ClassDeclaration Create(Identifier id) => new(id.Name);

        //[OperatorOverload(":")]
        //public static ClassDeclaration DefineBases(ClassDeclaration @class, Collection<Expression> bases)
        //{
        //    if (@class.Base is not null || @class.Interfaces is not null)
        //        throw new System.InvalidOperationException();

        //    IType @base;
        //    List<IType> interfaces;
        //    List<IType> types = new(bases.Select(Context.CurrentContext.Evaluate).Cast<IType>());

        //    if (types.Count == 0)
        //    {
        //        @base = null;
        //        interfaces = new();
        //    }
        //    else
        //    {
        //        if (!types[0].SRF.IsInterface)
        //        {
        //            @base = types[0];
        //            types.RemoveAt(0);
        //        }
        //        else
        //        {
        //            @base = null;
        //        }
        //        interfaces = new(types);
        //    }

        //    @class.Base = @base;
        //    @class.Interfaces = interfaces;

        //    return @class;
        //}

        [OperatorOverload(":")]
        public static ClassDeclaration DefineBases(ClassDeclaration @class, Expression @base)
        {
            if (@class.Base is not null)
                throw new System.InvalidOperationException();

            @class.Base = @base;

            return @class;
        }

        [SurroundingOperatorOverload("(", ")")]
        public static ClassDeclaration SetMetaClass(ClassDeclaration @class, Expression meta)
        {
            if (@class.MetaClass is not null)
                throw new System.InvalidOperationException();

            //if (!meta.SRF.IsSubclassOf(typeof(Class)))
            //{
            //    throw new System.ArgumentException(
            //        $"Metaclass {meta.Name} of class {@class.Name} must derive from {nameof(Class)}"
            //        );
            //}

            @class.MetaClass = meta;

            return @class;
        }

        [SurroundingOperatorOverload("{", "}")]
        public static ClassDeclaration Initialize(ClassDeclaration decl, Collection items)
        {
            if (decl.Items is not null) throw new System.InvalidOperationException();

            decl.Items = new(items.Cast<Expression>());

            return decl;
        }

        [SurroundingOperatorOverload("{", "}")]
        public static ClassDeclaration Initialize(ClassDeclaration decl)
        {
            return Initialize(decl, Collection.Empty);
        }

        public Expression Compile(ObjectDesciptorProcessor proc, Context ctx)
        {
            Type type;

            if (MetaClass is not null)
            {
                if ((Expression)proc.Process(MetaClass) is not IType metaclass)
                    throw new System.Exception($"{Name}'s metaclass expression evaluated to null.");

                type = metaclass.SRF.GetConstructor(
                    new[]
                    {
                        typeof(ClassDeclaration)
                    }
                    ).Invoke(new object[] { this }) as Type;
            }
            else
                type = new Type(this);

            //foreach (Expression item in Items)
            //{
            //    if (proc.Process(item) is INamedItem named) type.AddItem(named);
            //}

            return type;
        }
    }
}
