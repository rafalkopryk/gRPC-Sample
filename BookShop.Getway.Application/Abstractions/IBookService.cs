using BookShop.Common.Utils;
using BookShop.Getway.Application.Models.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Getway.Application.Abstractions
{
    public interface IBookService
    {
        Task<Result<IList<Book>>> GetBooks();
    }
}
