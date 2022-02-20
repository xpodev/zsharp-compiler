using System.Linq;
using ZSharp.OldCore;

namespace ZSharp.Engine
{
    internal class Evaluator : BaseProcessor
    {
        public Evaluator(Context ctx) : base(ctx) { }

        public override BuildResult<ErrorType, Expression?> Process(Expression expression) => Evaluate(expression);

        public BuildResult<ErrorType, ObjectInfo> Evaluate(ObjectInfo info) => Process(info);

        public BuildResult<ErrorType, Expression?> Evaluate(Expression expression) =>
            expression switch
            {
                //Identifier id => Context.Scope.GetItem<NamedItem>(id.Name) is NamedItem item
                //    ? new(item) : BuildResultUtils.Error($"Could not find item \'{id.Name}\'"),
                UnaryExpression unary => Evaluate(unary),
                BinaryExpression binary => Evaluate(binary),
                FunctionCall call => Evaluate(call),
                Keyword keyword => Evaluate(keyword),
                Collection collection => Evaluate(collection),
                _ => new(expression) //new($"Invaluable type: {expression.GetType().Name}", expression)
            };

        private BuildResult<ErrorType, Expression?> Evaluate(UnaryExpression unary) =>
            Invoke(unary, unary.Operator, unary.Operand);

        private BuildResult<ErrorType, Expression?> Evaluate(BinaryExpression binary) =>
            Invoke(binary, binary.Operator, binary.Left, binary.Right);

        private BuildResult<ErrorType, Expression?> Evaluate(FunctionCall call) =>
            Invoke(call, call.Name, call.Callable, call.Argument);

        private BuildResult<ErrorType, Expression?> Evaluate(Keyword keyword) =>
            Invoke(keyword, keyword.Name);

        private BuildResult<ErrorType, Expression?> Evaluate(Collection collection) =>
            BuildResultUtils.CombineResults(collection.Select(Evaluate)).Cast<Expression?>(array => new Collection(array));

        private BuildResult<ErrorType, Expression?> Invoke(Expression expression, string name, params ObjectInfo[] args)
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
