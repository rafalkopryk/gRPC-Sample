namespace BookShop.Common.Utils
{
    public class Result
    {
        protected Result()
        {
            IsSuccess = true;
        }

        protected Result(Error error)
        {
            IsFailure = true;
            Error = error;
        }

        public bool IsFailure { get; } = false;

        public bool IsSuccess { get; } = false;

        public Error Error { get; }

        public static Result Success()
        {
            return new Result();
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value);
        }

        public static Result Failure(Error error)
        {
            return new Result(error);
        }

        public static Result<T> Failure<T>(Error error)
        {
            return new Result<T>(error);
        }

        public static Result Failure(string description)
        {
            var error = new Error(ErrorCode.Unknown, description);
            return Result.Failure(error);
        }

        public static Result Failure(ErrorCode code, string description)
        {
            var error = new Error(code, description);
            return Result.Failure(error);
        }

        public static Result<T> Failure<T>(ErrorCode code, string description)
        {
            var error = new Error(code, description);
            return Failure<T>(error);
        }

        public static Result<T> Failure<T>(string description)
        {
            var error = new Error(ErrorCode.Unknown, description);
            return Failure<T>(error);
        }

    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public Result(T value) : base()
        {
            Value = value;
        }

        public Result(Error error) : base(error)
        {
        }
    }
}
