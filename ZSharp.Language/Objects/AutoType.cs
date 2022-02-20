namespace ZSharp.Language
{
    public class AutoType : Expression
    {
        public static AutoType Infer { get; } = new();

        public override string ToString()
        {
            return "auto";
        }
    }
}
