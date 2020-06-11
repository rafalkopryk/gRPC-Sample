namespace BookShop.BookService.Application.Handlers
{
    using BookShop.BookService.Domain;
    using BookShop.Common.Utils;
    using MediatR;

    public interface ICommandHandler<in T> : IRequestHandler<T, Result>
        where T : ICommand
    {
    }
}
