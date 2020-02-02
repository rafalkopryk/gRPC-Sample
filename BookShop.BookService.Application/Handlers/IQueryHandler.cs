using BookShop.BookService.Domain;
using BookShop.Common.Utils;
using MediatR;

namespace BookShop.BookService.Application.Handlers
{
    public interface IQueryHandler<in TQuery,TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    {
    }
}
