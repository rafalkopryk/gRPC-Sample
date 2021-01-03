namespace BookShop.BookService.Application.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Domain.Domain;
    using BookShop.BookService.Domain.Messages.Commands;
    using BookShop.Common.Utils;
    using Nest;

    using Result = BookShop.Common.Utils.Result;

    public class LockBookHandler : ICommandHandler<LockBook>
    {
        private readonly UnitOfWork unitOfWork;

        private readonly IElasticClient elasticClient;

        public LockBookHandler(UnitOfWork unitOfWork, IElasticClient elasticClient)
        {
            this.unitOfWork = unitOfWork;
            this.elasticClient = elasticClient;
        }

        public async Task<Result> Handle(LockBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new ErrorResult(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var book = await this.unitOfWork.Books
                .FindAsync(new object[] { request.BookId }, cancellationToken);

            if (book is null)
            {
                return Result.Failure(ErrorCode.NotFound, $"Book id:{request.BookId} does not exists");
            }

            var canChangeStatusOfBook = book.CanChangeStatus(BookStatus.Unavailable);
            if (!canChangeStatusOfBook.IsSuccess)
            {
                return Result.Failure($"Book id:{request.BookId} does not lock. {canChangeStatusOfBook.Error}");
            }

            book.ChangeStatus(BookStatus.Unavailable);

            await this.elasticClient.UpdateAsync<Book>(book.BookId, x => x.Doc(book), cancellationToken);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
