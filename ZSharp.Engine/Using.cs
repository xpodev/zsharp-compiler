using ZSharp.Core;


namespace ZSharp.Engine
{
    public static class Using
    {
        [KeywordOverload("using")]
        public static void Use(FunctionCall call)
        {
            //if (call.Callable is OperatorName op)
            //{
            //    if (call.Argument is Collection args)
            //    {

            //    }
            //}
        }

        [KeywordOverload("using")]
        public static void Use(Identifier id)
        {
            SearchScope currentScope = Context.CurrentContext.Scope.CurrentScope;
            foreach (var item in Context.CurrentContext.Scope.GetItem<Namespace>(id.Name))
            {
                currentScope.AddItem(item);
            }
        }

        [OperatorOverload(";", false)]
        public static Expression GetExpression(Expression e) => e;
    }
}
