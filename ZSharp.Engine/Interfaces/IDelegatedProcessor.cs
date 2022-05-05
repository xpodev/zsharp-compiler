﻿using ZSharp.Core;

namespace ZSharp.Engine
{
    public interface IDelegatedProcessor<T>
        where T : IDelegatedProcessor<T>
    {
        BuildResult<ErrorType, Node> Process(DelegateProcessor<T> proc);
    }
}