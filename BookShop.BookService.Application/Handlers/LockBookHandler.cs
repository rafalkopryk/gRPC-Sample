namespace BookShop.BookService.Application.Handlers
{
    using BookShop.BookService.Domain.Messages.Commands;
    using BookShop.BookService.Domain.ValueObjects;
    using BookShop.Common.Utils;
    using System.Threading;
    using System.Threading.Tasks;

    public class LockBookHandler : ICommandHandler<LockBook>
    {
        private readonly UnitOfWork _unitOfWork;

        public LockBookHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(LockBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new Error(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var book = await _unitOfWork.Books
                .FindAsync(new object[] { request.BookId }, cancellationToken)
                .ConfigureAwait(false);

            if (book is null)
                return Result.Failure(ErrorCode.NotFound, $"Book id:{request.BookId} does not exists");

            var canChangeStatusOfBook = book.CanChangeStatus(BookStatus.Unavailable);
            if (canChangeStatusOfBook.IsFailure)
                return Result.Failure($"Book id:{request.BookId} does not lock. {canChangeStatusOfBook.Error}");

            book.ChangeStatus(BookStatus.Unavailable);

            await _unitOfWork.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return Result.Success();
        }
    }
}
