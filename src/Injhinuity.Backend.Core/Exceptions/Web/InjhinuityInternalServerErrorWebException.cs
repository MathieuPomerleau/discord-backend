using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public class InjhinuityInternalServerErrorWebException : InjhinuityWebException
    {
        public InjhinuityInternalServerErrorWebException(string message) : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public InjhinuityInternalServerErrorWebException(string message, Exception innerException) : base(HttpStatusCode.InternalServerError, message, innerException)
        {
        }
    }
}
