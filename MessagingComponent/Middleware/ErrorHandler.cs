using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RealmDigital.MessagingService.Api.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                _logger.LogError(ex.Message, ex, context);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            foreach (var item in context.Request.Query)
            {
                ex.Data.Add($"QueryStringParam[{item.Key}]", item.Value.ToString());
            }

            ex.Data.Add("InnerExceptionDetails", ex.InnerException?.Message ?? "[null]");

            return ConfigureErrorResponse(context, HttpStatusCode.InternalServerError, "Something went wrong :( ");
        }

        private static Task ConfigureErrorResponse(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(message);
        }
    }
}
