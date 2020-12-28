namespace BookShop.BookService.Domain.Messages.Commands
{
    using System;

    public record AddBook(string Title, DateTime ReleaseDate)
        : ICommand;
}
