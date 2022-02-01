using System;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine
{
    internal static class BuildResultUtils
    {
        public static BuildResult<ErrorType, Expression?> Bind<T>(BuildResult<ErrorType, ObjectInfo> input, Func<T, BuildResult<string, Expression?>> func)
        {
            BuildResult<ErrorType, Expression?> result = new(input.Value);
            var output = func(
                input.Value.Expression is T expr 
                ? expr 
                : throw new ArgumentException($"input.Expression must be of type {typeof(T).Name}")
                );
            result.Errors.AddRange(output.Errors.Select(errorMessage => new ErrorType(errorMessage, input)));
            return result;
        }

        public static BuildResult<ErrorType, Expression?> Bind<T>(ObjectInfo info, Func<T, BuildResult<ErrorType, Expression?>> func)
        {
            BuildResult<ErrorType, Expression?> output = func(
                info.Expression is T expr 
                ? expr
                : throw new ArgumentException($"input.Expression must be of type {typeof(T).Name}")
                );
            return new(output.Value, output.Errors.Select(error => error.Object is null ? new(error.Message, info) : error));
        }

        /// <summary>
        /// Combines all the erros from <paramref name="result"/> and <paramref name="errors"/> into result and returns it
        /// </summary>
        /// <typeparam name="T">The type of the object held in the result</typeparam>
        /// <typeparam name="Ts">The type of the object held in params</typeparam>
        /// <param name="result">The destination result to insert errors into</param>
        /// <param name="errors">The errors to combine into result</param>
        /// <returns>The supplied result with all the combined error</returns>
        public static BuildResult<ErrorType, T> CombineErrors<T, Ts>(
            this BuildResult<ErrorType, T> result, 
            params BuildResult<ErrorType, Ts>[] errors
            )
            where T : class?
            where Ts : class?
        {
            Array.ForEach(errors, buildResult => result.Errors.AddRange(buildResult.Errors));
            return result;
        }

        public static BuildResult<T, U[]> CombineResults<T, U>(System.Collections.Generic.IEnumerable<BuildResult<T, U>> results)
            where U : class
        {
            System.Collections.Generic.List<T> errors = new();
            System.Collections.Generic.List<U> values = new();
            foreach (BuildResult<T, U> result in results)
            {
                values.Add(result.Value);
                errors.AddRange(result.Errors);
            }
            return new(values.ToArray(), errors);
        }

        public static BuildResult<T, U[]> CombineResults<T, U>(params BuildResult<T, U>[] results)
            where U : class
        {
            System.Collections.Generic.List<T> errors = new(results.Length);
            System.Collections.Generic.List<U> values = new(results.Length);
            foreach (BuildResult<T, U> result in results)
            {
                values.Add(result.Value);
                errors.AddRange(result.Errors);
            }
            return new(values.ToArray(), errors);
        }

        public static BuildResult<ErrorType, Expression?> Error(string errorMessage) => new(null, new ErrorType(errorMessage));

        public static BuildResult<ErrorType, Expression?> Error(this BuildResult<ErrorType, Expression?> result, string errorMessage) =>
            result.Error(new(errorMessage));

        //public static BuildResult<ErrorType, Expression?> Error(this BuildResult<ErrorType, Expression?> result, string errorMessage, ObjectInfo info) =>
        //    result.Error(new(errorMessage, info));
    }
}
