using ZSharp.Core;

namespace ZSharp.Engine
{
    public static class Static
    {
        private static T MakeStatic<T>(T mod)
            where T : IModifierProvider<string>
        {
            if (mod.HasModifier("static")) throw new System.Exception();

            mod.AddModifier("static");

            return mod;
        }

        //[KeywordOverload("static")]
        //public static SRFFunction MakeStatic(SRFFunction func)
        //{
        //    return MakeStatic<SRFFunction>(func);
        //}

        [KeywordOverload("static")]
        public static SRFFunctionBuilder MakeStatic(SRFFunctionBuilder func)
        {
            return MakeStatic<SRFFunctionBuilder>(func);
        }
    }
}
