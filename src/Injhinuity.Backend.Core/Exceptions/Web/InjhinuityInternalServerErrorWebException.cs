using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public class InjhinuityInternalServerErrorWebException : InjhinuityWebException
    {
        public InjhinuityInternalServerErrorWebException(string message, string reason) : base(HttpStatusCode.NotFound, message, reason)
        {
        }

        public InjhinuityInternalServerErrorWebException(string message, string reason, Exception innerException) : base(HttpStatusCode.NotFound, message, reason, innerException)
        {
        }
    }
}
