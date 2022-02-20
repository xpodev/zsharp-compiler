using System;
using ZSharp.OldCore;

namespace ZSharp.Engine.Errors
{
    public class ZSharpException : Exception
    {
        public FileInfo FileInfo { get; }

        public string InnerMessage { get; }

        public ZSharpException(FileInfo fileInfo, string message)
            : base($"{fileInfo.Document}({fileInfo.StartLine}, {fileInfo.StartColumn}): {message}")
        {
            InnerMessage = message;
        }
    }
}
