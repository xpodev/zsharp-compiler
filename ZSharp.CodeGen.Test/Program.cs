using ZSharp.CG;

namespace ZSharp.CodeGen.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModuleBuilder module = new("TestModule");

            var someType = module.DefineType("SomeType");
            var someMethod = someType.DefineMethod("SomeMethod", someType);
            someMethod.DefineParameter("someParameter", someType);
            someType.DefineField("someField", someType);

            module.DefineFunction("SomeGlobalFunction", someType);

            module.Write("Test.dll");
        }
    }
}
