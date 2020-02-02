using BookShop.Common.Utils;
using Grpc.Core;
using System;

namespace BookShop.BookService.Rpc.Utils
{
    public static class ResultExtensions
    {
        public static Status ToStatus(this Result result)
        {
            var statusCode = Enum.TryParse<StatusCode>(result.Error?.Code.ToString(), out var statusCodeValue)
                ? statusCodeValue
                : StatusCode.Unknown;

            return result.IsFailure
                ? new Status(statusCode, result.Error.Description)
                : new Status();
        }
    }
}
