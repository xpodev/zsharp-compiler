using System.Collections;
using System.Collections.Generic;
using ZSharp.Core;
using ZSharp.Engine.Cil;

namespace ZSharp.Engine
{
    public class FunctionBody 
        : Expression
        , IEnumerable<ILInstruction>
    {
        private readonly List<ILInstruction> _body = new();

        private readonly ILBuilder _ilGenerator;

        public Function Function { get; }

        public FunctionBody(Function owner)
        {
            _ilGenerator = new(Function = owner, _body);
        }

        public ILBuilder GetILGenerator() => _ilGenerator;

        public IEnumerator<ILInstruction> GetEnumerator()
        {
            return ((IEnumerable<ILInstruction>)_body).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_body).GetEnumerator();
        }
    }
}
