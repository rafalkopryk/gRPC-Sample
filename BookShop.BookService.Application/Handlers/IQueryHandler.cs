namespace BookShop.BookService.Application.Handlers
{
    using BookShop.BookService.Domain;
    using BookShop.Common.Utils;

    using MediatR;

    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    {
    }
}
