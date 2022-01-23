using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;
using ZSharp.Engine.Cil;

namespace ZSharp.Engine
{
    public class IL 
        : Expression
        , IILGenerator
        , IEnumerable<ILInstruction>
    {
        private readonly List<ILInstruction> _instructions = new();

        public IL(params ILInstruction[] instructions)
        {
            _instructions = new(instructions);
        }

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
                if ((Expression)Context.CurrentContext.Evaluate(expr) is ILInstruction ilInst)
                    il.Add(ilInst);
            }
            return il;
        }

        public void Add(ILInstruction instruction) => _instructions.Add(instruction);

        public IEnumerator<ILInstruction> GetEnumerator()
        {
            return ((IEnumerable<ILInstruction>)_instructions).GetEnumerator();
        }

        public IEnumerable<ILInstruction> GetIL()
        {
            return _instructions;
        }

        public override string ToString()
        {
            return "IL";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_instructions).GetEnumerator();
        }
    }
}
