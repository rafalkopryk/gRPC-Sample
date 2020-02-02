using BookShop.Common.Utils;
using MediatR;

namespace BookShop.Getway.Application.Messages
{
    public interface ICommand : IRequest<Result>
    {
    }
}
