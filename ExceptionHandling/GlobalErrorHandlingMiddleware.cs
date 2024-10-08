using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                var crudException = new CustomException(HttpStatusCode.InternalServerError, ex.Message, "An unexpected error occurred.", new List<object>());
                await HandleExceptionAsync(context, crudException);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, CustomException exception)
        {
            var response = new
            {
                code = (int)exception.Status,
                message = exception.Message,
                data = exception.Data
            };

            var result = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.Status;

            return context.Response.WriteAsync(result);
        }


    }
}
