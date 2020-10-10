namespace BookShop.Getway.Application.Abstractions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Models.Books;

    public interface IBookProvider
    {
        Task<Result<IReadOnlyList<Book>>> GetBooks();
    }
}
