namespace BookShop.Getway.Rest.Utils
{
    using System;
    using System.Collections.Generic;

    using BookShop.Common.Utils;

    public class Envelope
    {
        protected Envelope(Status status, IReadOnlyList<ErrorResult> errors)
        {
            this.Status = status;
            this.Errors = errors;
            this.TimeGenerated = DateTime.UtcNow;
        }

        public Status Status { get; }

        public IReadOnlyList<ErrorResult> Errors { get; }

        public DateTime TimeGenerated { get; }

        public static Envelope Ok()
        {
            return new Envelope(Status.Success, null);
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, Status.Success, null);
        }

        public static Envelope Error(params ErrorResult[] errors)
        {
            return new Envelope(Status.Fault, errors);
        }

        public static Envelope Error(Exception exception)
        {
            var error = new ErrorResult(ErrorCode.Internal, exception?.Message);
            return Error(error);
        }
    }
}
