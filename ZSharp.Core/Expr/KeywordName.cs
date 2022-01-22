namespace ZSharp.Core
{
    public class KeywordName : FunctionName
    {
        public string Keyword { get; set; }

        public KeywordName(string keyword) : base("kw_" + keyword)
        {
            Keyword = keyword;
        }
    }
}
