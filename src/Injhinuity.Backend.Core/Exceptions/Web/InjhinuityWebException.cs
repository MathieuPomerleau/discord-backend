using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public abstract class InjhinuityWebException : InjhinuityException
    {
        public HttpStatusCode StatusCode { get; }
        public string Reason { get; }

        public InjhinuityWebException(HttpStatusCode statusCode, string message, string reason) : base(message)
        {
            StatusCode = statusCode;
            Reason = reason;
        }

        public InjhinuityWebException(HttpStatusCode statusCode, string message, string reason, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            Reason = reason;
        }
    }
}
