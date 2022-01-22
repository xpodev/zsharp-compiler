using System;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class MemberAccess
    {
        [OperatorOverload(".")]
        public static INamedItem GetMember(SearchScope container, Identifier id)
        {
            return container.GetItem(id.Name);
        }

        [OperatorOverload(".")]
        public static INamedItem GetMember(IType type, Identifier id)
        {
            return type.GetMember(id.Name);
        }
    }
}
