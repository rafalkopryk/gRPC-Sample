using BookShop.BookService.Domain;
using BookShop.Common.Utils;
using MediatR;

namespace BookShop.BookService.Application.Handlers
{
    public interface ICommandHandler<in T> : IRequestHandler<T, Result>
        where T : ICommand
    {
    }
}
