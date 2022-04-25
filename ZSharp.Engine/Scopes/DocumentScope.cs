using System.Collections.Generic;

namespace ZSharp.Engine
{
    public class DocumentScope : ScopeBase
    {
        private readonly Stack<IContext> _scopeStack = new();

        public IContext Global { get; } = new ScopeBase();

        public IContext Current => _scopeStack.Peek();

        public void EnterScope(IContext scope) => _scopeStack.Push(scope);

        public void LeaveScope(IContext scope) => _scopeStack.Pop();
    }
}
