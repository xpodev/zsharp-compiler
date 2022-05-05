namespace ZSharp.Core
{
    public record class Literal(object Value) : Expression
    {
        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }

    public record class Literal<T> : Literal
    {
        public new T Value { get; set; }

        public Literal(T value) : base(value)
        {
            Value = value;
        }
    }
}
