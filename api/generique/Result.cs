using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.generique
{
    public class Result<T>
    {
        public T Value { get; private set; }

        public string Error { get; private set; }
        public bool IsSuccess => Error == null;

        protected Result(T value, string error)
        {
            Value = value;
            Error = error;

        }
        public static Result<T> Success(T value) => new Result<T>(value, null);
        public static Result<T> Failure(string error) => new Result<T>(default(T), error);
    }
}
