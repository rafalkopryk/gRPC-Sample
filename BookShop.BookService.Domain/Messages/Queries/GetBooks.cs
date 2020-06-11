namespace BookShop.BookService.Domain.Messages.Queries
{
    using BookShop.BookService.Domain.Domain;
    using System.Collections.Generic;

    public class GetBooks : IQuery<IReadOnlyList<Book>>
    {
    }
}
