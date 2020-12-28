namespace BookShop.BookService.Domain.Messages.Commands
{
    public record ArchiveBook(int BookId)
        : ICommand;
}
