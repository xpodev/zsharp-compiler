﻿using System;

namespace ZSharp.Core
{
    internal static class FuncUtils
    {
        public static Func<In, Out> Compose<In, _, Out>(Func<In, _> left, Func<_, Out> right) =>
            input => right(left(input));
    }
}
