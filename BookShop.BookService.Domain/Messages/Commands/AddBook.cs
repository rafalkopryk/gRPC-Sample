namespace BookShop.BookService.Domain.Messages.Commands
{
    using System;

    public class AddBook : ICommand
    {
        public AddBook(string title, DateTime releaseDate)
            => (this.Title, this.ReleaseDate) = (title, releaseDate);

        public string Title { get; }

        public DateTime ReleaseDate { get; }
    }
}
