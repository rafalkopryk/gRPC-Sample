namespace BookShop.Getway.Application.Handlers.Books
{
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.BookService.Rpc;
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Messages.Commands;

    public class ArchiveBookHandler : ICommandHandler<ArchiveBook>
    {
        private readonly BookRpc.BookRpcClient bookServiceClient;

        public ArchiveBookHandler(BookRpc.BookRpcClient bookServiceClient)
        {
            this.bookServiceClient = bookServiceClient;
        }

        public async Task<Result> Handle(ArchiveBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new ErrorResult(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var archiveBookRequest = new ArchiveBookRequest
            {
                Id = request.Id,
            };

            await this.bookServiceClient
                .ArchiveBookAsync(archiveBookRequest, cancellationToken: cancellationToken);

            return Result.Success();
        }
    }
}
