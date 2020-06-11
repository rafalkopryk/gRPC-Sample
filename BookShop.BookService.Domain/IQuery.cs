namespace BookShop.BookService.Domain
{
    using BookShop.Common.Utils;
    using MediatR;

    public interface IQuery<T> : IRequest<Result<T>>
    {
    }
}
