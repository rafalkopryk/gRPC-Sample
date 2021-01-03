namespace BookShop.Getway.Application.Handlers.Books
{
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Application;
    using BookShop.BookService.Application.Handlers;
    using BookShop.BookService.Domain.Domain;
    using BookShop.BookService.Domain.Messages.Commands;
    using BookShop.Common.Utils;
    using Nest;

    using Result = BookShop.Common.Utils.Result;

    public class AddBookHandler : ICommandHandler<AddBook>
    {
        private readonly UnitOfWork unitOfWork;

        private readonly IElasticClient elasticClient;

        public AddBookHandler(UnitOfWork unitOfWork, IElasticClient elasticClient)
        {
            this.unitOfWork = unitOfWork;
            this.elasticClient = elasticClient;
        }

        public async Task<Result> Handle(AddBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new ErrorResult(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var book = new Book(request.Title, request.ReleaseDate);

            this.unitOfWork.Books.Add(book);

            await this.elasticClient.IndexDocumentAsync(book, cancellationToken);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
