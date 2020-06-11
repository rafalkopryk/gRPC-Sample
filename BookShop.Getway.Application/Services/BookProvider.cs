namespace BookShop.Getway.Application
{
    using BookShop.BookService.Rpc;
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Abstractions;
    using BookShop.Getway.Application.Mappers.Books;
    using BookShop.Getway.Application.Models.Books;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BookProvider : IBookProvider
    {
        private readonly BookRpc.BookRpcClient _bookServiceClient;

        public BookProvider(BookRpc.BookRpcClient bookRpcClient)
            => _bookServiceClient = bookRpcClient;

        public async Task<Result<IReadOnlyList<Book>>> GetBooks()
        {
            var getBooksResult = await _bookServiceClient.GetBooksAsync(new GetBooksRequest());

            var result = getBooksResult
                .Books
                .ToBookModels();

            return Result.Success(result);
        }
    }
}
