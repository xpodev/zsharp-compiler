﻿using ZSharp.OldCore;

namespace ZSharp.Engine
{
    public interface IObjectDescriptor
    {
        BuildResult<ErrorType, Expression?> Compile(ObjectDesciptorProcessor proc, Context ctx);
    }
}