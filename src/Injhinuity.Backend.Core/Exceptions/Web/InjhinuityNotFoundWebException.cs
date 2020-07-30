using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public class InjhinuityNotFoundWebException : InjhinuityWebException
    {
        public InjhinuityNotFoundWebException(string message, string reason) : base(HttpStatusCode.NotFound, message, reason)
        {
        }

        public InjhinuityNotFoundWebException(string message, string reason, Exception innerException) : base(HttpStatusCode.NotFound, message, reason, innerException)
        {
        }
    }
}
