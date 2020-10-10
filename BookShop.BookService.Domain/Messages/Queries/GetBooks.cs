namespace BookShop.BookService.Domain.Messages.Queries
{
    using System.Collections.Generic;

    using BookShop.BookService.Domain.Domain;

    public class GetBooks : IQuery<IReadOnlyList<Book>>
    {
    }
}
