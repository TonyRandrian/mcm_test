using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this._next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await this.HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            ApiResponse<bool> response = new()
            {
                Success = false,
                Data = false
            };
            switch (exception)
            {
                case BadRequestException e:
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    break;

                case NotFoundException e:
                    response.Code = (int)HttpStatusCode.NotFound;
                    response.Message = exception.Message;
                    break;

                case UnauthorizedException e:
                    response.Code = (int)HttpStatusCode.Unauthorized;
                    response.Message = exception.Message;
                    break;

                case DomainException e:
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    break;

                default:
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Message = exception.Message;
                    break;
            }
            var result = JsonSerializer.Serialize(response);
            context.Response.StatusCode = response.Code;
            return context.Response.WriteAsync(result);
        }
    }
}
