namespace ZSharp
{
    public class Unit
    {
        public static Unit Value { get; } = new();

        private Unit()
        {

        }

        public static Unit GetUnit() => Value;

        [Engine.KeywordOverload("()")]
        public static Engine.IType GetUnitType() => Engine.Context.CurrentContext.TypeSystem.Unit;
    }
}
