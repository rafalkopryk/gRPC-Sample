namespace BookShop.Common.Utils
{
    using System.Text.Json.Serialization;

    public class ErrorResult
    {
        [JsonConstructor]
        public ErrorResult(ErrorCode code, string description)
        {
            this.Description = description;
            this.Code = code;
        }

        public string Description { get; }

        public ErrorCode Code { get; }
    }
}
