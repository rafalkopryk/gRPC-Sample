namespace BookShop.Getway.Application.Handlers.Books
{
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Rpc;
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Messages.Commands;

    using Google.Protobuf.WellKnownTypes;

    public class AddBookHandler : ICommandHandler<AddBook>
    {
        private readonly BookRpc.BookRpcClient bookServiceClient;

        public AddBookHandler(BookRpc.BookRpcClient bookServiceClient)
        {
            this.bookServiceClient = bookServiceClient;
        }

        public async Task<Result> Handle(AddBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new ErrorResult(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var addBookRequest = new AddBookRequest
            {
                Title = request.Title,
                ReleaseDate = Timestamp.FromDateTime(request.ReleaseDate),
            };

            await this.bookServiceClient
                .AddBookAsync(addBookRequest, cancellationToken: cancellationToken);

            return Result.Success();
        }
    }
}
