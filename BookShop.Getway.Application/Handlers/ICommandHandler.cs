using BookShop.Common.Utils;
using BookShop.Getway.Application.Messages;
using MediatR;

namespace BookShop.Getway.Application.Handlers
{
    public interface ICommandHandler<T> : IRequestHandler<T, Result>
        where T : ICommand
    {
    }
}
