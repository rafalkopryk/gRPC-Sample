namespace BookShop.Getway.Application.Handlers.Books
{
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Rpc;
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Messages.Commands;

    public class LockBookHandler : ICommandHandler<LockBook>
    {
        private readonly BookRpc.BookRpcClient bookServiceClient;

        public LockBookHandler(BookRpc.BookRpcClient bookServiceClient)
        {
            this.bookServiceClient = bookServiceClient;
        }

        public async Task<Result> Handle(LockBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new ErrorResult(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var lockBookRequest = new LockBookRequest
            {
                Id = request.Id,
            };

            await this.bookServiceClient
                .LockBookAsync(lockBookRequest, cancellationToken: cancellationToken);

            return Result.Success();
        }
    }
}
