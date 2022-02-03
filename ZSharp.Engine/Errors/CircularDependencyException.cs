using System;
using System.Collections.Generic;

namespace ZSharp.Engine
{
    public class CircularDependencyException<T> : Exception
    {
        public List<T> Chain { get; } = new();

        public override string Message => base.Message + " " + string.Join(" -> ", Chain);

        public CircularDependencyException()
            : base($"Circular dependency detected:") { }
    }
}
