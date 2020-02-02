using System.Threading;
using System.Threading.Tasks;
using BookShop.BookService.Rpc;
using BookShop.Common.Utils;
using BookShop.Getway.Application.Messages.Commands;

namespace BookShop.Getway.Application.Handlers.Books
{
    public class ArchiveBookHandler : ICommandHandler<ArchiveBook>
    {
        private readonly BookRpc.BookRpcClient _bookServiceClient;

        public ArchiveBookHandler(BookRpc.BookRpcClient bookServiceClient)
        {
            this._bookServiceClient = bookServiceClient;
        }

        public async Task<Result> Handle(ArchiveBook request, CancellationToken cancellationToken)
        {
            var archiveBookRequest = new ArchiveBookRequest
            {
                Id = request.Id
            };

            await _bookServiceClient
                .ArchiveBookAsync(archiveBookRequest, cancellationToken: cancellationToken);

            return Result.Success();
        }
    }
}
