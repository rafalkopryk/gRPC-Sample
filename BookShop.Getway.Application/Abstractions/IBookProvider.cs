namespace BookShop.Getway.Application.Abstractions
{
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Models.Books;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookProvider
    {
        Task<Result<IReadOnlyList<Book>>> GetBooks();
    }
}
