using System;
using ZSharp.Core;

namespace ZSharp.Engine
{
    internal class Evaluator : BaseProcessor<string>
    {
        public Evaluator(Context ctx) : base(ctx) { }

        public override Result<string, ObjectInfo> Process(ObjectInfo @object) => Evaluate(@object);

        public Result<string, ObjectInfo> Evaluate(ObjectInfo @object)
        {
            Result<string, Expression> result = Evaluate(@object.Expression);
            return new(result.Error, new(@object.FileInfo, result.Object));
        }

        public Result<string, Expression> Evaluate(Expression expression) =>
            expression switch
            {
                Identifier id => Context.Scope.GetItem<NamedItem>(id.Name) is NamedItem item
                    ? new(item) : new(expression),// new($"Could not find item \'{id.Name}\'", expression),
                UnaryExpression unary => Evaluate(unary),
                BinaryExpression binary => Evaluate(binary),
                FunctionCall call => Evaluate(call),
                KeywordName keyword => Context.Scope.GetItem<IInvocable>(keyword.Name) is IInvocable invocable
                    ? invocable.Invoke()
                    : new($"Could not evaluate keyword \'{keyword.Name}\'", expression),
                _ => new(expression) //new($"Invaluable type: {expression.GetType().Name}", expression)
            };

        private Result<string, Expression> Evaluate(UnaryExpression unary)
        {
            if (unary is null)
                throw new ArgumentNullException(nameof(unary));

            IInvocable invocable = Context.Scope.GetItem<IInvocable>(unary.Operator.Name);
            if (invocable is null) return new($"Could not evaluate unary operator \'{unary.Operator}\'", unary);

            Result<string, Expression> argument = Evaluate(unary.Operand.Expression);
            if (argument.IsFailure) return new($"Could not evaluate operand \'{unary.Operand}\'", unary);

            return invocable.Invoke(argument.Object);
        }

        private Result<string, Expression> Evaluate(BinaryExpression binary)
        {
            if (binary is null)
                throw new ArgumentNullException(nameof(binary));

            IInvocable invocable = Context.Scope.GetItem<IInvocable>(binary.Operator.Name);
            if (invocable is null) return new($"Could not evaluate binary operator \'{binary.Operator}\'", binary);

            Result<string, Expression> left = Evaluate(binary.Left.Expression);
            if (left.IsFailure) return new($"Could not evaluate left operand \'{binary.Left}\'", binary);

            Result<string, Expression> right = Evaluate(binary.Right.Expression);
            if (right.IsFailure) return new($"Could not evaluate right operand \'{binary.Right}\'", binary);

            return invocable.Invoke(left.Object, right.Object);
        }

        private Result<string, Expression> Evaluate(FunctionCall call)
        {
            if (call is null)
                throw new ArgumentNullException(nameof(call));

            Result<string, Expression> callable = Evaluate(call.Callable.Expression);
            if (callable.IsFailure) return new($"\'{call.Callable}\' is not callable", call);

            IInvocable invocable = Context.Scope.GetItem<IInvocable>(call.Name);
            if (invocable is null) return new($"Could not evaluate call operator \'{call.Name}\'", call);

            Result<string, Expression> argument = Evaluate(call.Argument.Expression);
            if (argument.IsFailure) return new($"Could not evaluate operand \'{call.Argument}\'", call);

            return invocable.Invoke(callable.Object, argument.Object);
        }
    }
}
