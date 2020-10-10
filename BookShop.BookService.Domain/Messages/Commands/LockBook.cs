namespace BookShop.BookService.Domain.Messages.Commands
{
    public class LockBook : ICommand
    {
        public LockBook(int bookId)
           => this.BookId = bookId;

        public int BookId { get; }
    }
}
