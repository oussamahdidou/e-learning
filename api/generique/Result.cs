using System;

namespace api.generique
{
    public class Result
    {
        public string? Error { get; private set; }
        public bool IsSuccess => Error == null;

        protected Result(string? error)
        {
            Error = error;
        }

        public static Result Success() => new Result(null);
        public static Result Failure(string error) => new Result(error);
    }

    public class Result<T> : Result
    {
        public T? Value { get; private set; }

        protected Result(T? value, string? error) : base(error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(value, null);
        public static new Result<T> Failure(string error) => new Result<T>(default(T), error);
    }
}
