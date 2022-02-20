using ZSharp.OldCore;

namespace ZSharp.Engine.ZSLib
{
    public class MemberAccess
    {
        [OperatorOverload(".")]
        public static INamedItem GetMember(IMemberContainer container, Identifier id) => 
            container.GetMember(id.Name);

        [OperatorOverload(".")]
        public static INamedItem GetMember(Expression expression, Identifier id)
        {
            IMemberContainer? container;
            if (expression is Identifier containerId) 
                container = Context.CurrentContext!.Scope.GetItem<IMemberContainer>(containerId.Name);
            else
                container = Context.CurrentContext!.Evaluate(expression) as IMemberContainer;

            if (container is null) throw new System.Exception();

            return GetMember(container, id);
        }
    }
}
