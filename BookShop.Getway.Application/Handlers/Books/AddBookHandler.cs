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
        private readonly BookRpc.BookRpcClient _bookServiceClient;

        public AddBookHandler(BookRpc.BookRpcClient bookServiceClient)
        {
            _bookServiceClient = bookServiceClient;
        }

        public async Task<Result> Handle(AddBook request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result.Failure(new Error(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var addBookRequest = new AddBookRequest
            {
                Title = request.Title,
                ReleaseDate = Timestamp.FromDateTime(request.ReleaseDate)
            };

            await _bookServiceClient
                .AddBookAsync(addBookRequest, cancellationToken: cancellationToken);

            return Result.Success();
        }
    }
}
