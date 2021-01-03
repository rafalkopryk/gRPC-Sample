namespace BookShop.BookService.Application.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Domain.Domain;
    using BookShop.BookService.Domain.Messages.Commands;
    using BookShop.Common.Utils;
    using Nest;

    using Result = Common.Utils.Result;

    public class ArchiveBookHandler : ICommandHandler<ArchiveBook>
    {
        private readonly UnitOfWork unitOfWork;

        private readonly IElasticClient elasticClient;

        public ArchiveBookHandler(UnitOfWork unitOfWork, IElasticClient elasticClient)
        {
            this.unitOfWork = unitOfWork;
            this.elasticClient = elasticClient;
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

            await this.elasticClient.UpdateAsync<Book>(book.BookId, x => x.Doc(book), cancellationToken);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
