namespace BookShop.Getway.Application.Models.Books
{
    using System;

    public class Book
    {
        public int Id { get; }

        public string Title { get; }

        public DateTime ReleaseDate { get; }

        public string Status { get; }

        public Book(int id, string title, DateTime releaseDate, string status)
            => (Id, Title, ReleaseDate, Status) = (id, title, releaseDate, status);
    }
}
