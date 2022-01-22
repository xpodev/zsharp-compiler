using System;
using System.Reflection;

namespace ZSharp.Engine
{
    public abstract class BaseProcessor : Core.IExpressionProcessor
    {
        public Context Context { get; }

        public event Action OnPreProcess;

        public event Action OnPostProcess;

        public BaseProcessor(Context ctx)
        {
            Context = ctx;
        }

        #region Dynamic Invocation

        public MethodInfo GetMethod(string keyword, params System.Type[] types)
        {
            return Context.Scope.GetItem<SRFFunctionOverload>(keyword)?.Get(types)?.SRF;
        }

        public Core.Expression InvokeUnaryOperator(string @operator, Core.Expression expression)
        {
            MethodInfo method = GetMethod(@operator, expression.GetType());
            if (method is null) return null;
            return method.Invoke(null, new object[] { expression }) as Core.Expression;
        }

        public Core.Expression InvokeBinaryOperator(string @operator, Core.Expression left, Core.Expression right)
        {
            MethodInfo method = GetMethod(@operator, new System.Type[] { left.GetType(), right.GetType() });
            if (method is null) return null;
            return method.Invoke(null, new object[] { left, right }) as Core.Expression;
        }

        public Core.Expression InvokeKeyword(string keyword, Core.Expression arg)
        {
            return GetMethod(keyword, arg.GetType()).Invoke(null, new object[] { arg }) as Core.Expression;
        }

        #endregion

        #region Processing

        public virtual void PreProcess() { OnPreProcess?.Invoke(); }

        public Core.ObjectInfo Process(Core.ObjectInfo @object)
        {
            Process(@object.Expression);
            return @object;
        }

        public virtual Core.Expression Process(Core.Expression expr)
        {
            return expr switch
            {
                Core.Identifier id =>
                    Context.CurrentContext.Scope.GetItem(id.Name) ?? expr,
                Core.KeywordName kw =>
                    GetMethod(kw.Name, System.Type.EmptyTypes)
                        .Invoke(null, Array.Empty<object>()) as Core.Expression,
                Core.UnaryExpression unary =>
                    InvokeUnaryOperator(
                        unary.Operator.Name,
                        Process(unary.Operand.Expression)
                        ),
                Core.BinaryExpression binary =>
                    InvokeBinaryOperator(
                        binary.Operator.Name,
                        Process(binary.Left.Expression),
                        Process(binary.Right.Expression)
                        ),
                //Core.Collection collection => collection,
                Core.FunctionCall call => Call(call),
                Core.Keyword keyword =>
                    InvokeKeyword(keyword.KeywordName, Process(keyword.SubExpression)),
                _ => expr
            } ?? expr;
        }

        public virtual void PostProcess() { OnPostProcess?.Invoke(); }

        #endregion

        private Core.Expression Call(Core.FunctionCall call)
        {
            MethodInfo method;
            Core.Expression callable = Process(call.Callable).Expression;
            if (callable is null) return call;
            Core.Expression arg = Process(call.Argument).Expression;
            if (arg is Core.Collection args)
            {
                if (args.Items.Count == 0)
                {
                    method = GetMethod(call.Name, callable.GetType());
                    if (method is null) return call;
                    if (method.IsStatic)
                        return method.Invoke(null, new object[] { callable }) as Core.Expression;
                    else
                        return method.Invoke(callable, Array.Empty<object>()) as Core.Expression;
                }
                //if (args.Items.Count == 1) arg = args.Items[0];
                //else arg = new Core.Collection<Core.Expression>(args.Items);
            }
            method = GetMethod(call.Name, callable.GetType(), arg.GetType());
            if (method is null) return call;
            if (method.IsStatic)
                return method.Invoke(null, new object[] { callable, arg }) as Core.Expression;
            else
                return method.Invoke(callable, new object[] { arg }) as Core.Expression;
        }
    }
}
