using BookShop.BookService.Rpc;
using BookShop.Getway.Application.Models.Books;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.Getway.Application.Mappers.Books
{
    public static class BooksExtensions
    {
        public static Book ToBookModel(this GetBooksReply.Types.Book book)
           => new Book(book.Id, book.Title, book.ReleaseDate.ToDateTime(), book.Status);

        public static IList<Book> ToBookModels(this RepeatedField<GetBooksReply.Types.Book> books)
            => books.Select(x => ToBookModel(x)).ToList();
    }
}
