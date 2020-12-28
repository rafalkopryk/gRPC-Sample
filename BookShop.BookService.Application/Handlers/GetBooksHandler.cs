namespace BookShop.BookService.Application.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Domain.Domain;
    using BookShop.BookService.Domain.Messages.Queries;
    using BookShop.Common.Utils;
    using Microsoft.EntityFrameworkCore;

    public class GetBooksHandler : IQueryHandler<GetBooks, IReadOnlyList<Book>>
    {
        private readonly UnitOfWork unitOfWork;

        public GetBooksHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<Book>>> Handle(GetBooks request, CancellationToken cancellationToken)
        {
            var books = await this.unitOfWork
                .Books
                .Where(x => x.Status.Status != StatusBookEnum.Archive)
                .AsNoTracking()
                .ToListAsync() as IReadOnlyList<Book>;

            return Result.Success(books);
        }
    }
}
