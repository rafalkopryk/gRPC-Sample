namespace BookShop.Common.Utils
{
    public class Error
    {
        public Error(string description, ErrorCode code)
        {
            Description = description;
            Code = code;
        }

        public string Description { get; private set; }

        public ErrorCode Code { get; private set; }
    }
}
