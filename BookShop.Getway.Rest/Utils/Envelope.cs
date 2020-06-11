namespace BookShop.Getway.Rest.Utils
{
    using BookShop.Common.Utils;
    using System;
    using System.Collections.Generic;

    public sealed class Envelope<T> : Envelope
    {
        public T Result { get; }

        public Envelope(T result, Status status, IReadOnlyList<Error> errors) : base(status, errors)
        {
            Result = result;
        }
    }

    public class Envelope
    {
        public Status Status { get; }

        public IReadOnlyList<Error> Errors { get; }
        
        public DateTime TimeGenerated { get; }

        protected Envelope(Status status, IReadOnlyList<Error> errors)
        {
            Status = status;
            Errors = errors;
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope Ok()
        {
            return new Envelope(Status.Success, null);
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, Status.Success, null);
        }

        public static Envelope Error(params Error[] errors)
        {
            return new Envelope(Status.Fault, errors);
        }

        public static Envelope Error(Exception exception)
        {
            var error = new Error(ErrorCode.Internal, exception?.Message);
            return Error(error);
        }
    }
}
