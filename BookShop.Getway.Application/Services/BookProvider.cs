namespace BookShop.Getway.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.BookService.Rpc;
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Abstractions;
    using BookShop.Getway.Application.Mappers.Books;
    using BookShop.Getway.Application.Models.Books;

    internal class BookProvider : IBookProvider
    {
        private readonly BookRpc.BookRpcClient bookServiceClient;

        public BookProvider(BookRpc.BookRpcClient bookRpcClient)
            => this.bookServiceClient = bookRpcClient;

        public async Task<Result<IReadOnlyList<Book>>> GetBooks()
        {
            var getBooksResult = await this.bookServiceClient.GetBooksAsync(new GetBooksRequest());

            var result = getBooksResult
                .Books
                .ToBookModels();

            return Result.Success(result);
        }
    }
}
