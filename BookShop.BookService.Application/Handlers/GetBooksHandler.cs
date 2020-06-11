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
    using static BookShop.BookService.Domain.ValueObjects.BookStatus;

    public class GetBooksHandler : IQueryHandler<GetBooks, IReadOnlyList<Book>>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetBooksHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<Book>>> Handle(GetBooks request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork
                .Books
                .Where(x => x.Status.Status != StatusBookEnum.Archive)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false) as IReadOnlyList<Book>;

            return Result.Success(books);
        }
    }
}
