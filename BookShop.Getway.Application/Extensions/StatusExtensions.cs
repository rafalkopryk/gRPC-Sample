namespace BookShop.Getway.Application.Extensions
{
    using System;

    using BookShop.Common.Utils;

    using Grpc.Core;

    public static class StatusExtensions
    {
        public static ErrorResult ToError(this Status status)
        {
            var errorCode = Enum.TryParse<ErrorCode>(status.StatusCode.ToString(), out var errorCodeValue)
                ? errorCodeValue
                : ErrorCode.Unknown;

            return new ErrorResult(errorCode, status.Detail);
        }
    }
}
