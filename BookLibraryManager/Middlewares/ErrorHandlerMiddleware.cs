using BookLibraryManagerBL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookLibraryManager.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            
            catch(UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = 400;
                await HandleExceptionAsync(context,
                    ex,
                    HttpStatusCode.Unauthorized,
                    ex.Message);
            }
            
            catch(ArgumentException ex)
            {
                context.Response.StatusCode = 400;
                await HandleExceptionAsync(context,
                    ex,
                    HttpStatusCode.BadRequest,
                    "Bad Request");
            }

            catch(Exception ex)
            {
                await HandleExceptionAsync(context,
                    ex,
                    HttpStatusCode.InternalServerError,
                    "Internal server error");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode httpStatusCode, string message)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            ErrorDto errorDto = new()
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

            await context.Response.WriteAsJsonAsync(errorDto);
        }
    }
}
