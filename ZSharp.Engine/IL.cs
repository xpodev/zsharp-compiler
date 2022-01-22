using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;
using ZSharp.Engine.Cil;

namespace ZSharp.Engine
{
    public class IL 
        : Collection
        , IILGenerator
    {
        public IL(params ILInstruction[] instructions) : base(instructions) { }

        [KeywordOverload("IL")]
        public static IL CreateILContext()
        {
            return new IL();
        }

        //[OperatorOverload("{}()")]
        [SurroundingOperatorOverload("{", "}")]
        public static IL Initialize(IL il, Collection exprs)
        {
            foreach (Expression expr in exprs)
            {
                if (Context.CurrentContext.Evaluate(expr) is ILInstruction ilInst)
                    il.Items.Add(ilInst);
            }
            return il;
        }

        public IEnumerable<ILInstruction> GetIL()
        {
            return Items.Cast<ILInstruction>();
        }

        public override string ToString()
        {
            return "IL";
        }
    }
}
