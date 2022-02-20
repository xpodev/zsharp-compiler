using System;
using System.Collections.Generic;

namespace ZSharp.Core
{
    public class BuildResult<ErrorT, ValueT>
        where ValueT : class
    {
        public List<ErrorT> Errors { get; } = new();

        public ValueT Value { get; private set; }

        public bool HasValue => Value is not null;

        public bool HasErrors => Errors.Count > 0;

        public BuildResult(ValueT value)
        {
            Value = value;
        }

        public BuildResult(ValueT value, ErrorT error)
            : this(value)
        {
            Errors.Add(error);
        }

        public BuildResult(ValueT value, IEnumerable<ErrorT> errors)
            : this(value)
        {
            Errors = new(errors);
        }

        public BuildResult<ErrorT, ValueT> Error(ErrorT error)
        {
            Errors.Add(error);
            return this;
        }

        public BuildResult<ErrorT, T> Cast<T>(Func<ValueT, T> cast) where T : class => new(cast(Value), Errors);

        public BuildResult<ErrorT, ValueT> Then(Func<ValueT, BuildResult<ErrorT, ValueT>> func)
        {
            BuildResult<ErrorT, ValueT> result = func(Value);
            Errors.AddRange(result.Errors);
            return this;
        }

        public BuildResult<ErrorT, T> Return<T>(T value) where T : class => new(value, Errors);

        public static implicit operator ValueT(BuildResult<ErrorT, ValueT> input) => input.Value;

        public override string ToString()
        {
            return HasErrors ? $"{Errors.Count} Errors" : Value.ToString();
        }
    }
}
