namespace BookShop.Getway.Rest.Utils
{
    using System.Collections.Generic;

    using BookShop.Common.Utils;

    public sealed class Envelope<T> : Envelope
    {
        public Envelope(T result, Status status, IReadOnlyList<ErrorResult> errors)
           : base(status, errors)
        {
            this.Result = result;
        }

        public T Result { get; }
    }
}
