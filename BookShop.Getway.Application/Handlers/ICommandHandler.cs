namespace BookShop.Getway.Application.Handlers
{
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Messages;
    using MediatR;

    public interface ICommandHandler<in T> : IRequestHandler<T, Result>
        where T : ICommand
    {
    }
}
