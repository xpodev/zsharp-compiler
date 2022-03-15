using ZSharp.Core;

namespace ZSharp.Engine
{
    internal class DocumentProcessor : BaseProcessor
    {
        public DocumentProcessor(Context ctx) : base(ctx) { }

        public override BuildResult<ErrorType, Expression?> Process(Expression expression) =>
            expression switch
            {
                UnaryExpression unary => Process(unary),
                BinaryExpression binary => Process(binary),
                FunctionCall call => Process(call),
                Keyword keyword => Process(keyword),
                _ => new(expression) //new($"Invaluable type: {expression.GetType().Name}", expression)
            };

        private BuildResult<ErrorType, Expression?> Process(UnaryExpression unary) =>
            Invoke(unary, unary.Operator, unary.Operand);

        private BuildResult<ErrorType, Expression?> Process(BinaryExpression binary) =>
            Invoke(binary, binary.Operator, binary.Left, binary.Right);

        private BuildResult<ErrorType, Expression?> Process(FunctionCall call) =>
            Invoke(call, call.Name, call.Callable, call.Argument);

        private BuildResult<ErrorType, Expression?> Process(Keyword keyword) =>
            Invoke(keyword, keyword.Name);

        private BuildResult<ErrorType, Expression?> Invoke(Expression expression, string name, params NodeInfo[] args)
        {
            BuildResult<ErrorType, Expression[]> argsResult = BuildResultUtils.CombineResults(args.Map(Process).Map(result => result.Return(result.Value.Expression)));
            BuildResult<ErrorType, Expression?> result = new(expression, argsResult.Errors);

            IInvocable? invocable = Context.Scope.GetItem<IInvocable>(name);

            if (invocable is null)
                result.Error($"Could not evaluate operator \'{name}\'");
            if (result.HasErrors) return result.Return<Expression?>(null);

            return BuildResultUtils.CombineErrors(invocable!.Invoke(argsResult.Value), result);
        }
    }
}
