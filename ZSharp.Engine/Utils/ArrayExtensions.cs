using System;

namespace ZSharp.Engine
{
    internal static class ArrayExtensions
    {
        public static Out[] Map<In, Out>(this In[] array, Func<In, Out> func)
        {
            Out[] result = new Out[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = func(array[i]);
            }
            return result;
        }
    }
}
