using System.Collections.Generic;

namespace ZSharp.Engine
{
    public class DocumentScope : ScopeBase
    {
        private readonly Stack<IScope> _scopeStack = new();

        public IScope Global { get; } = new ScopeBase();

        public IScope Current => _scopeStack.Peek();

        public void EnterScope(IScope scope) => _scopeStack.Push(scope);

        public void LeaveScope(IScope scope) => _scopeStack.Pop();
    }
}
