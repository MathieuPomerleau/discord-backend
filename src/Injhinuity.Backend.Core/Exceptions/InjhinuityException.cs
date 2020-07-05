using System;

namespace Injhinuity.Backend.Core.Exceptions
{
    public class InjhinuityException : Exception
    {
        public InjhinuityException(string message) : base(message) {}

        public InjhinuityException(string message, Exception innerException) : base(message, innerException) {}
    }
}
