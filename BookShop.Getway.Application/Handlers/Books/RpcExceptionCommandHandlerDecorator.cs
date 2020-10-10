namespace BookShop.Getway.Application.Handlers.Books
{
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Extensions;
    using BookShop.Getway.Application.Messages;

    using Grpc.Core;

    public class RpcExceptionCommandHandlerDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly ICommandHandler<T> commandHandler;

        public RpcExceptionCommandHandlerDecorator(ICommandHandler<T> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public async Task<Result> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                return await this.commandHandler.Handle(request, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (RpcException exception)
            {
                var error = exception.Status.ToError();
                return Result.Failure(error);
            }
        }
    }
}
