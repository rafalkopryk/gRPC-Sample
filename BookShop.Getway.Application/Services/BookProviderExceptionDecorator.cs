namespace BookShop.Getway.Application.Services
{
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Abstractions;
    using BookShop.Getway.Application.Extensions;
    using BookShop.Getway.Application.Models.Books;
    using Grpc.Core;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BookProviderExceptionDecorator : IBookProvider
    {
        private readonly IBookProvider _bookProvider;

        public BookProviderExceptionDecorator(IBookProvider bookService)
        {
            _bookProvider = bookService;
        }

        public async Task<Result<IReadOnlyList<Book>>> GetBooks()
        {
            try
            {
                return await _bookProvider.GetBooks().ConfigureAwait(false);
            }
            catch (RpcException exception)
            {
                var error = exception.Status.ToError();
                return Result.Failure<IReadOnlyList<Book>>(error);
            }
        }
    }
}
