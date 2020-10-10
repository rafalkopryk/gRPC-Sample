namespace BookShop.BookService.Domain
{
    using BookShop.Common.Utils;

    using MediatR;

#pragma warning disable CA1040 // Avoid empty interfaces
    public interface IQuery<T> : IRequest<Result<T>>
#pragma warning restore CA1040 // Avoid empty interfaces
    {
    }
}
