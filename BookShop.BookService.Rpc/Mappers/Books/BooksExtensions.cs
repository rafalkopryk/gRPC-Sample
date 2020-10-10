namespace BookShop.BookService.Rpc.Mappers.Books
{
    using System.Collections.Generic;
    using System.Linq;

    using BookShop.BookService.Domain.Domain;
    using Google.Protobuf.Collections;
    using Google.Protobuf.WellKnownTypes;

    public static class BooksExtensions
    {
        public static GetBooksReply.Types.Book ToRpcModel(this Book book)
        {
            if (book is null)
            {
                return null;
            }

            return new GetBooksReply.Types.Book
            {
                Id = book.BookId,
                Title = book.Title,
                ReleaseDate = Timestamp.FromDateTime(book.ReleaseDate),
                Status = book.Status?.Status.ToString(),
            };
        }

        public static RepeatedField<GetBooksReply.Types.Book> ToRpcModel(this IEnumerable<Book> books)
        {
            var convertedBooks = books
                .Select(x => x.ToRpcModel())
                .Where(x => x is not null)
                .ToList();

            return new RepeatedField<GetBooksReply.Types.Book>() { convertedBooks };
        }
    }
}
