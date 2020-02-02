using BookShop.BookService.Rpc;
using BookShop.Common.Utils;
using BookShop.Getway.Application.Abstractions;
using BookShop.Getway.Application.Mappers.Books;
using BookShop.Getway.Application.Models.Books;
using Grpc.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Getway.Application
{
    public class BookService : IBookService
    {
        private readonly BookRpc.BookRpcClient _bookServiceClient;

        public BookService(BookRpc.BookRpcClient bookRpcClient)
            => _bookServiceClient = bookRpcClient;

        public async Task<Result<IList<Book>>> GetBooks()
        {
            var getBooksResult = await _bookServiceClient.GetBooksAsync(new GetBooksRequest());

            var result = getBooksResult
                .Books
                .ToBookModels();

            return Result.Success(result);
        }
    }
}
