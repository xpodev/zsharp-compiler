namespace ZSharp.Engine
{
    public class TypeSystem
    {
        public static TypeSystem Current => Context.CurrentContext.TypeSystem;

        public IType Void { get; }

        public IType Object { get; }

        public IType Unit { get; }

        public IType String { get; }

        public IType Int32 { get; }

        public IType Int64 { get; }

        public GenericTypeOverload ValueTuple { get; }

        internal TypeSystem(Context ctx)
        {
            Namespace system = ctx.Scope.GetItem<Namespace>("System");
            Namespace zsharp = ctx.Scope.GetItem<Namespace>("ZSharp");

            Void = GetType(system, typeof(void).Name);
            Object = GetType(system, typeof(object).Name);
            String = GetType(system, typeof(string).Name);
            Int32 = GetType(system, typeof(int).Name);
            Int64 = GetType(system, typeof(long).Name);
            ValueTuple = GetGenericType(system, typeof(System.ValueTuple).Name);
            
            Unit = GetType(zsharp, "Unit");
        }

        private static IType GetType(Namespace ns, string name, params IType[] types)
        {
            return ns.GetItem<GenericTypeOverload>(name).Get(types);
        }

        private static GenericTypeOverload GetGenericType(Namespace ns, string name)
        {
            return ns.GetItem<GenericTypeOverload>(name);
        }

        [KeywordOverload("void")]
        public static IType GetVoid() =>
            Current.Void;

        [KeywordOverload("string")]
        public static IType GetString() =>
            Current.String;

        [KeywordOverload("i32")]
        public static IType GetInt32() =>
            Current.Int32;

        [KeywordOverload("i64")]
        public static IType GetInt64() =>
            Current.Int64;
    }
}
