using System;

namespace ZSharp.Core
{
    public static class FuncUtils
    {
        public static Func<In, Out> Compose<In, _, Out>(Func<In, _> left, Func<_, Out> right) =>
            input => right(left(input));

        public static T Identity<T>(T input) => input;

        public static Out Identity<In, Out>(In input)
            where In : Out => input;
    }
}
