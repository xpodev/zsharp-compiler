using System;
using ZSharp.Engine;

namespace ZSharp.Language.EngineExt
{
    internal static class ScopeBaseExtenstions
    {
        public static T GetObject<T>(this IContext ctx, FullyQualifiedName fqn)
            where T : class, IContext, INamedObject
        {
            foreach (NodeInfo<Identifier> part in fqn.Parts)
            {
                if (ctx.GetObject(part.Object.Name) is IContext ns)
                {
                    ctx = ns;
                }
                else break;
            }
            return ctx as T;
        }
    }
}
