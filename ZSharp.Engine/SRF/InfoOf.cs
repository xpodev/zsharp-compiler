using System.Collections.Generic;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class InfoOf : Expression
    {
        private readonly List<Expression> _objects = new();
        private static readonly FunctionReference _infoFunction;

        private static readonly InfoOf _infoOf = new();

        public static T GetObject<T>(int id) where T : Expression => 
            _infoOf._objects[id] as T;

        [KeywordOverload("infoof")]
        public static InfoOf GetInfoObject() => _infoOf;

        [SurroundingOperatorOverload("(", ")")]
        public static IL GetInfoOf(InfoOf info, Expression expression)
        {
            int id;
            if ((id = info._objects.IndexOf(expression)) == -1)
            {
                id = info._objects.Count;
                info._objects.Add(expression);
            }
            return new IL()
            {
                Cil.ILOpCodes.LoadInt32(id),
                Cil.ILOpCodes.CallFunction(_infoFunction.MakeGeneric(new TypeReference(expression.GetType())))
            };
        }

        static InfoOf()
        {
            System.Type type = typeof(InfoOf);
            var method = type.GetMethod(nameof(GetObject), new System.Type[] { typeof(int) });
            _infoFunction = new FunctionReference(method);
        }
    }
}
