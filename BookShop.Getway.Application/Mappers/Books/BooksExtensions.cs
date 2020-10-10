namespace BookShop.Getway.Application.Mappers.Books
{
    using System.Collections.Generic;
    using System.Linq;

    using BookShop.BookService.Rpc;
    using BookShop.Getway.Application.Models.Books;
    using Google.Protobuf.Collections;

    public static class BooksExtensions
    {
        public static Book ToBookModel(this GetBooksReply.Types.Book book)
           => new Book(book.Id, book.Title, book.ReleaseDate.ToDateTime(), book.Status);

        public static IReadOnlyList<Book> ToBookModels(this RepeatedField<GetBooksReply.Types.Book> books)
            => books.Select(x => ToBookModel(x)).ToList();
    }
}
