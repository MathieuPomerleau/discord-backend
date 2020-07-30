using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public class InjhinuityBadRequestWebException : InjhinuityWebException
    {
        public InjhinuityBadRequestWebException(string message, string reason) : base(HttpStatusCode.NotFound, message, reason)
        {
        }

        public InjhinuityBadRequestWebException(string message, string reason, Exception innerException) : base(HttpStatusCode.NotFound, message, reason, innerException)
        {
        }
    }
}
