using BookShop.Common.Utils;
using MediatR;

namespace BookShop.BookService.Domain
{
    public interface IQuery<T> : IRequest<Result<T>>
    {
    }
}
