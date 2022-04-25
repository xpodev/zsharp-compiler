﻿namespace ZSharp.Core
{
    public record class Identifier(string Name) : Expression
    {
        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
