namespace BookShop.Getway.Application.Messages.Commands
{
    using System;

    public record AddBook(string Title, DateTime ReleaseDate)
        : ICommand;
}
