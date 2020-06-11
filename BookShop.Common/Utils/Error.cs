namespace BookShop.Common.Utils
{
    public class Error
    {
        public Error(ErrorCode code, string description)
        {
            Description = description;
            Code = code;
        }

        public string Description { get; }

        public ErrorCode Code { get; }
    }
}
