using System;

namespace Columbus.InnerSource.Core.Commands
{
    public class Result : IResult
    {
        public bool Success { get; }
        public Exception Error { get; }

        protected internal Result(bool success)
        {
            Success = success;
        }
        protected internal Result(Exception error)
        {
            Success = false;
            Error = error;
        }

        public static IResult Fail(Exception error) => new Result(error);
        public static IResult<T> Fail<T>(Exception error) => new Result<T>(error);
        public static IResult Ok() => new Result(true);
        public static IResult<T> Ok<T>(T value) => new Result<T>(value, true);
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Value { get; }

        protected internal Result(T value, bool success)
            : base(success)
        {
            Value = value;
        }
        protected internal Result(Exception error)
            : base(error)
        {
        }
    }
}
