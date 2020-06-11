namespace BookShop.BookService.Domain
{
    using BookShop.Common.Utils;
    using MediatR;

    public interface ICommand : IRequest<Result>
    {
    }
}
