namespace BookShop.BookService.Application.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Domain.Domain;
    using BookShop.BookService.Domain.Messages.Commands;
    using BookShop.Common.Utils;

    public class ArchiveBookHandler : ICommandHandler<ArchiveBook>
    {
        private readonly UnitOfWork unitOfWork;

        public ArchiveBookHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ArchiveBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new ErrorResult(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var book = await this.unitOfWork.Books
                .FindAsync(new object[] { request.BookId }, cancellationToken);

            if (book is null)
            {
                return Result.Failure(ErrorCode.NotFound, $"Book id:{request.BookId} does not not exists");
            }

            book.ChangeStatus(BookStatus.Archive);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
