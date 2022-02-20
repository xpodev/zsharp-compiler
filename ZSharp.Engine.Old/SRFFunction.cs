using System.Linq;
using System.Reflection.Emit;
using System.Collections.Generic;
using ZSharp.OldCore;
using Mono.Cecil;

namespace ZSharp.Engine
{
    public static class SRFFunction
        //: NamedItem
        //, IModifierProvider<string>
        //, ICompilable
    {
        private const string Keyword = "func";

        //public static SRFFunctionBuilder CreateFunction(TypedName def)
        //{
        //    if (def.Type is not FunctionType type)
        //        type = new FunctionType(def.Type, Context.CurrentContext.TypeSystem.Void);
        //    return new(def.Name, type);
        //}

        //[OperatorOverload("{}()")]
        //public static SRFFunction Initialize(SRFFunction func, Collection<Expression> exprs)
        //{
        //    //Core.Function func = new Core.Function(sig);

        //    //Core.Cil.IL body = new();

        //    //foreach (Expression expr in exprs)
        //    //{
        //    //    if (Context.CurrentContext.ProcessExpression(expr) is ILExpression il)
        //    //        body.Items.AddRange(il.GetIL().Items);
        //    //}

        //    //func.Body = body;
        //    if (func._body is not null) throw new System.InvalidOperationException();

        //    func._body = exprs.Items.ToArray();
        //    return func;
        //}

        //[OperatorOverload("()()")]
        //public static SRFFunction GetOverload(SRFFunctionOverload funcs, Collection<Expression> args)
        //{
        //    //System.Type[] types;
        //    //if (args.Items.Count == 0)
        //    //{
        //    //    types = new[] { typeof(Unit) };
        //    //} else
        //    //{
        //    //    types = args.Select(Context.CurrentContext.Process).Cast<Type>().Select(t => t.SRF).ToArray();
        //    //}
        //    //return new(funcs.Get(
        //    //    types
        //    //    ));
        //    return null;
        //}

        //[OperatorOverload("()()")]
        [SurroundingOperatorOverload("(", ")")]
        public static IFunction GetOverload(FunctionOverload funcs, Collection args)
        {
            return funcs.Get(args.Select(Context.CurrentContext.Evaluate).Cast<IType>().ToArray());
        }

        [SurroundingOperatorOverload("(", ")")]
        public static IFunction GetOverload(FunctionOverload funcs, IType type) => 
            funcs.Get(type);

        [SurroundingOperatorOverload("(", ")")]
        public static IFunction GetOverload(FunctionOverload funcs) =>
            funcs.Get(System.Array.Empty<IType>());
    }
}
