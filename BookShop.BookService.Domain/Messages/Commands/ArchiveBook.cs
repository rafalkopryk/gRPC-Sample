namespace BookShop.BookService.Domain.Messages.Commands
{
    public class ArchiveBook : ICommand
    {
        public ArchiveBook(int bookId)
           => this.BookId = bookId;

        public int BookId { get; }
    }
}
