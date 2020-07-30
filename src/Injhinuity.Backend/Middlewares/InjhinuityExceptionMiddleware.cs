using System;
using System.Net;
using System.Threading.Tasks;
using Grpc.Core;
using Injhinuity.Backend.Core.Exceptions;
using Injhinuity.Backend.Core.Exceptions.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Injhinuity.Backend.Middlewares
{
    public class InjhinuityExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<InjhinuityExceptionMiddleware> _logger;

        public InjhinuityExceptionMiddleware(RequestDelegate next, ILogger<InjhinuityExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RpcException ex)
            {
                _logger.LogError(ex.Message);

                var (statusCode, reason) = ex.StatusCode switch
                {
                    StatusCode.AlreadyExists => (HttpStatusCode.BadRequest, "Resource already exists"),
                    StatusCode.NotFound => (HttpStatusCode.NotFound, "Resource does not exist"),
                    StatusCode.Unavailable => (HttpStatusCode.ServiceUnavailable, "Service is unavailable"),
                    _ => throw new ArgumentException("Provided status code is not handled")
                };

                await CreateWrapperAndWriteAsync(context, ex.GetType().Name, ex.Message, reason, statusCode);
            }
            catch (InjhinuityWebException ex) when
                (ex is InjhinuityNotFoundWebException ||
                 ex is InjhinuityBadRequestWebException ||
                 ex is InjhinuityInternalServerErrorWebException)
            {
                await CreateWrapperAndWriteAsync(context, ex.GetType().Name, ex.Message, ex.Reason, ex.StatusCode);
            }
        }

        private async Task CreateWrapperAndWriteAsync(HttpContext context, string typeName, string message, string reason, HttpStatusCode statusCode)
        {
            var wrapper = CreateExceptionWrapper(typeName, message, reason, statusCode);
            var result = JsonConvert.SerializeObject(wrapper);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)wrapper.StatusCode;

            await context.Response.WriteAsync(result);
        }

        private ExceptionWrapper CreateExceptionWrapper(string name, string message, string reason, HttpStatusCode statusCode) =>
            new ExceptionWrapper
            {
                Name = name,
                Message = message,
                Reason = reason,
                StatusCode = statusCode
            };
    }
}
