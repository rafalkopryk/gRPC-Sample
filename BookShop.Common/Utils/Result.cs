namespace BookShop.Common.Utils
{
    public class Result
    {
        protected Result()
        {
        }

        protected Result(ErrorResult error)
        {
            this.Error = error;
        }

        public bool IsSuccess => this.Error is null;

        public ErrorResult Error { get; }

        public static Result Success()
        {
            return new Result();
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value);
        }

        public static Result Failure(ErrorResult error)
        {
            return new Result(error);
        }

        public static Result<T> Failure<T>(ErrorResult error)
        {
            return new Result<T>(error);
        }

        public static Result Failure(string description)
        {
            var error = new ErrorResult(ErrorCode.Unknown, description);
            return Failure(error);
        }

        public static Result Failure(ErrorCode code, string description)
        {
            var error = new ErrorResult(code, description);
            return Failure(error);
        }

        public static Result<T> Failure<T>(ErrorCode code, string description)
        {
            var error = new ErrorResult(code, description);
            return Failure<T>(error);
        }

        public static Result<T> Failure<T>(string description)
        {
            var error = new ErrorResult(ErrorCode.Unknown, description);
            return Failure<T>(error);
        }
    }
}
