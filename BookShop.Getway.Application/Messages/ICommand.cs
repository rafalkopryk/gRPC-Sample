namespace BookShop.Getway.Application.Messages
{
    using BookShop.Common.Utils;
    using MediatR;

    public interface ICommand : IRequest<Result>
    {
    }
}
