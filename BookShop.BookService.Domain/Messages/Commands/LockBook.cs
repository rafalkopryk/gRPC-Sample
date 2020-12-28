namespace BookShop.BookService.Domain.Messages.Commands
{
    public record LockBook(int BookId)
        : ICommand;
}
