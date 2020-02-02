namespace BookShop.BookService.Domain.Messages.Commands
{
    public class LockBook : ICommand
    {
        public LockBook(int bookId)
           => BookId = bookId;

        public int BookId { get; }
    }
}
