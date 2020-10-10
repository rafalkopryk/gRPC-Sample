namespace BookShop.Getway.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Abstractions;
    using BookShop.Getway.Application.Extensions;
    using BookShop.Getway.Application.Models.Books;

    using Grpc.Core;

    internal class BookProviderExceptionDecorator : IBookProvider
    {
        private readonly IBookProvider bookProvider;

        public BookProviderExceptionDecorator(IBookProvider bookService)
        {
            this.bookProvider = bookService;
        }

        public async Task<Result<IReadOnlyList<Book>>> GetBooks()
        {
            try
            {
                return await this.bookProvider.GetBooks().ConfigureAwait(false);
            }
            catch (RpcException exception)
            {
                var error = exception.Status.ToError();
                return Result.Failure<IReadOnlyList<Book>>(error);
            }
        }
    }
}
