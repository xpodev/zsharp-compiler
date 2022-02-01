namespace ZSharp.Engine
{
    public class FunctionType
        : TypeReference
        , IType
    {
        //[OperatorOverload("->")]
        //public static FunctionType CreateFunctionType(Expression inputE, Expression outputE)
        //{
        //    Type input = inputE as Type;
        //    if (input is null && inputE is Collection<Expression> coll)
        //    {
        //        if (coll.Items.Count == 0) input = new TupleType();
        //    }
        //    Type output = outputE as Type;
        //    if (output is null && outputE is FullyQualifiedName fqn)
        //    {
        //        output = new TypeName(fqn.Name, fqn.Namespace);
        //    }
        //    return new FunctionType(input, output);
        //}

        public IType Input { get; }

        public IType Output { get; }

        public FunctionType(IType input, IType output)
            : base(
                  output.Name == "Void" ?
                  typeof(System.Action<>).MakeGenericType(input.SRF) :
                  typeof(System.Func<,>).MakeGenericType(input.SRF, output.SRF)
                  )
        {
            Input = input;
            Output = output;
        }

        //[OperatorOverload("->")]
        //public static FunctionType CreateFunctionType(IType input, IType output)
        //{
        //    return new(input, output);
        //}

        //[OperatorOverload("->")]
        //public static FunctionType CreateFunctionType(Collection input, IType output)
        //{
        //    input = input.Select(Context.CurrentContext.Evaluate)
        //        .Cast<Collection>().First();
        //    IType[] inputTypes = input.Cast<IType>().ToArray();
        //    return new(
        //        Context.CurrentContext.TypeSystem.ValueTuple.Get(inputTypes), output
        //        );
        //}
    }
}
