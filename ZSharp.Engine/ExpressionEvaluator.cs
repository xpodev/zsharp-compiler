using ZSharp.Core;

namespace ZSharp.Engine
{
    public class ExpressionEvaluator
    {
        public ProjectScope Context { get; }

        public ExpressionEvaluator(ProjectScope context)
        {
            Context = context;
        }

        public BuildResult<ErrorType, object> Evaluate(Expression expression) =>
            expression switch
            {
                Identifier id => Evaluate(id).Cast(FuncUtils.Identity<object>),
                Literal literal => new(literal.Value),
                BinaryExpression binary => Evaluate(binary),
                UnaryExpression unary => Evaluate(unary),
                _ => new(expression, new ErrorType("Could not evaluate expression")),
            };

        public BuildResult<ErrorType, INamedObject> Evaluate(Identifier identifier) =>
            Context.Document.GetObject(identifier.Name) is INamedObject result
            ? new(result)
            : new(null, new ErrorType($"Could not find object '{identifier.Name}'"));

        public BuildResult<ErrorType, object> Evaluate(BinaryExpression binary)
        {
            BuildResult<ErrorType, object> result = new(null);

            BuildResult<ErrorType, object> left = Evaluate(binary.Left.Object);
            if (left.HasErrors) result.Errors.AddRange(left.Errors);

            BuildResult<ErrorType, object> right = Evaluate(binary.Right.Object);
            if (right.HasErrors) result.Errors.AddRange(right.Errors);
            
            if (result.HasErrors) return result;

            return (Context.Document.GetObject($"_{binary.Operator}_") as IInvocable).Invoke(left.Value, right.Value)
                .CombineErrors(result);
        }

        public BuildResult<ErrorType, object> Evaluate(UnaryExpression unary)
        {
            BuildResult<ErrorType, object> result = new(null);

            BuildResult<ErrorType, object> operand = Evaluate(unary.Operand.Object);
            if (operand.HasErrors) result.Errors.AddRange(operand.Errors);

            if (result.HasErrors) return result;

            string operatorName = unary.IsPrefix ? $"{unary.Operator}_" : $"_{unary.Operator}";

            return (Context.Document.GetObject(operatorName) as IInvocable).Invoke(operand.Value)
                .CombineErrors(result);
        }
    }
}
