namespace BookShop.BookService.Rpc.Extensions
{
    using System;

    using BookShop.Common.Utils;
    using Grpc.Core;

    public static class ResultExtensions
    {
        public static Status ToStatus(this Result result)
        {
            var statusCode = Enum.TryParse<StatusCode>(result.Error?.Code.ToString(), out var statusCodeValue)
                ? statusCodeValue
                : StatusCode.Unknown;

            return !result.IsSuccess
                ? new Status(statusCode, result.Error.Description)
                : default;
        }
    }
}
