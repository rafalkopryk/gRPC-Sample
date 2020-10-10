namespace BookShop.BookService.Domain.ValueObjects
{
    public partial class BookStatus
    {
        public enum StatusBookEnum
        {
            Available,
            Unavailable,
            Archive,
        }
    }
}
