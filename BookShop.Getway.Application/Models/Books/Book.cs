namespace BookShop.Getway.Application.Models.Books
{
    using System;
    using System.Text.Json.Serialization;

    public class Book
    {
        [JsonConstructor]
        public Book(int id, string title, DateTime releaseDate, string status)
            => (this.Id, this.Title, this.ReleaseDate, this.Status) = (id, title, releaseDate, status);

        public int Id { get; }

        public string Title { get; }

        public DateTime ReleaseDate { get; }

        public string Status { get; }
    }
}
