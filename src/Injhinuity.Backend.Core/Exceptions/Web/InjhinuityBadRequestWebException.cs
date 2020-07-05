using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public class InjhinuityBadRequestWebException : InjhinuityWebException
    {
        public InjhinuityBadRequestWebException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }

        public InjhinuityBadRequestWebException(string message, Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException)
        {
        }
    }
}
