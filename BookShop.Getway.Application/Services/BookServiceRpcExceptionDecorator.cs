using BookShop.Common.Utils;
using BookShop.Getway.Application.Abstractions;
using BookShop.Getway.Application.Extensions;
using BookShop.Getway.Application.Models.Books;
using Grpc.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Getway.Application.Services
{
    public class BookServiceRpcExceptionDecorator : IBookService
    {
        private readonly IBookService _bookService;

        public BookServiceRpcExceptionDecorator(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Result<IList<Book>>> GetBooks()
        {
            try
            {
                return await _bookService.GetBooks();
            }
            catch (RpcException exception)
            {
                var error = exception.Status.ToError();
                return Result.Failure<IList<Book>>(error);
            }
        }
    }
}
