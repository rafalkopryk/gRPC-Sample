using BookShop.Common.Utils;
using MediatR;

namespace BookShop.BookService.Domain
{
    public interface ICommand : IRequest<Result>
    {
    }
}
