﻿using ZSharp.Core;

namespace ZSharp.Engine
{
    public abstract class NamedItem 
        : Expression
        , INamedItem
    {
        public string Name { get; }

        public NamedItem(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return GetType().Name + " " + Name;
        }
    }
}