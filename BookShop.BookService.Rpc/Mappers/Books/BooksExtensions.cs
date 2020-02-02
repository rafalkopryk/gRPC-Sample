using BookShop.BookService.Domain.Domain;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.BookService.Rpc.Mappers.Books
{
    public static class BooksExtensions
    {
        public static GetBooksReply.Types.Book ToRpcModel(this Book book)
           => new GetBooksReply.Types.Book
           {
               Id = book.BookId,
               Title = book.Title,
               ReleaseDate = Timestamp.FromDateTime(book.ReleaseDate),
               Status = book.Status?.Status.ToString()
           };

        public static RepeatedField<GetBooksReply.Types.Book> ToRpcModel(this IEnumerable<Book> books)
        {
            var convertedBooks = books
                .Select(x => x.ToRpcModel())
                .ToList();
            
            return new RepeatedField<GetBooksReply.Types.Book>() { convertedBooks };
        }
    }
}
