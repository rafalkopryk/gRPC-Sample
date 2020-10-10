namespace BookShop.Common.Utils
{
    using System.Text.Json.Serialization;

    public class Result<T> : Result
    {
        public Result(T value)
            : base()
        {
            this.Value = value;
        }

        [JsonConstructor]
        public Result(T value, ErrorResult error)
            : base(error)
        {
            this.Value = value;
        }

        public Result(ErrorResult error)
            : base(error)
        {
        }

        public T Value { get; }
    }
}
