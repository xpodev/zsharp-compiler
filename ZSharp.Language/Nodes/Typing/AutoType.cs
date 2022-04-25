namespace ZSharp.Language
{
    public record class AutoType : Type
    {
        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }

        public override TypeName AsTypeName()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "auto";
        }
    }
}
