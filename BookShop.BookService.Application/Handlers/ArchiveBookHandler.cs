using BookShop.BookService.Domain.Messages.Commands;
using BookShop.BookService.Domain.ValueObjects;
using BookShop.Common.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.BookService.Application.Handlers
{
    public class ArchiveBookHandler : ICommandHandler<ArchiveBook>
    {
        private readonly UnitOfWork _unitOfWork;

        public ArchiveBookHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ArchiveBook request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books
                .FindAsync(new object[] { request.BookId }, cancellationToken)
                .ConfigureAwait(false);

            if (book is null)
                return Result.Failure(ErrorCode.NotFound, $"Book id:{request.BookId} does not not exists");

            book.ChangeStatus(BookStatus.Archive);

            await _unitOfWork.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return Result.Success();
        }
    }
}
