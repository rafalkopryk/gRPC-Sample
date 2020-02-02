using BookShop.BookService.Domain.Domain;
using System.Collections.Generic;

namespace BookShop.BookService.Domain.Messages.Queries
{
    public class GetBooks : IQuery<IReadOnlyList<Book>>
    {
    }
}
