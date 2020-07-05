using System;
using System.Net;

namespace Injhinuity.Backend.Core.Exceptions.Web
{
    public class InjhinuityNotFoundWebException : InjhinuityWebException
    {
        public InjhinuityNotFoundWebException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }

        public InjhinuityNotFoundWebException(string message, Exception innerException) : base(HttpStatusCode.NotFound, message, innerException)
        {
        }
    }
}
