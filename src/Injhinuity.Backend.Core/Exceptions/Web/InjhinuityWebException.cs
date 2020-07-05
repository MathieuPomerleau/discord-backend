using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public abstract class InjhinuityWebException : InjhinuityException
    {
        public HttpStatusCode StatusCode { get; }

        public InjhinuityWebException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public InjhinuityWebException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
