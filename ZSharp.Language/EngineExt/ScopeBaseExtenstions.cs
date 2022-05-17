using System;
using ZSharp.Engine;

namespace ZSharp.Language.EngineExt
{
    internal static class ScopeBaseExtenstions
    {
        public static T GetObject<T>(this IScope ctx, FullyQualifiedName fqn)
            where T : class, IScope, INamedObject
        {
            foreach (NodeInfo<Identifier> part in fqn.Parts)
            {
                if (ctx.GetObject(part.Object.Name) is IScope ns)
                {
                    ctx = ns;
                }
                else break;
            }
            return ctx as T;
        }
    }
}
