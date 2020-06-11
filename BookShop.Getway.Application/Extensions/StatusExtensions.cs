namespace BookShop.Getway.Application.Extensions
{
    using BookShop.Common.Utils;
    using Grpc.Core;
    using System;

    public static class StatusExtensions
    {
        public static Error ToError(this Status status)
        {
            var errorCode = Enum.TryParse<ErrorCode>(status.StatusCode.ToString(), out var errorCodeValue)
                ? errorCodeValue
                : ErrorCode.Unknown;

            return new Error(errorCode, status.Detail);
        }
    }
}
