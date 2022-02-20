namespace ZSharp.OldCore
{
    public struct Result<ErrorT, ObjectT>
        where ErrorT : class
        where ObjectT : class
    {
        public ErrorT Error { get; }

        public ObjectT Object { get; }

        public bool IsSuccess => Error is null;

        public bool IsFailure => Error is not null;

        public Result(ErrorT error, ObjectT @object)
        {
            Error = error;
            Object = @object;
        }

        public Result(ObjectT @object)
            : this(null, @object)
        {
        }

        public static implicit operator ObjectT(Result<ErrorT, ObjectT> result) => result.IsSuccess ? result.Object : null;
    }
}
