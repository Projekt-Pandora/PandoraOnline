using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pandora.Server.WebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.WebServices.ErrorHandling
{
    public static class WebServiceErrorHandlingExtensions
    {
        public static WebApplication UseErrorHandling(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler("/error");

            webApplication.Map("error", (HttpContext httpContext) =>
            {
                var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>()!;
                var exception = exceptionHandlerFeature.Error;
                var statusCodeException = exception as StatusCodeException;
                var statusCode = statusCodeException?.StatusCode ?? StatusCodes.Status500InternalServerError;

                var errorMessage = new ErrorGetDto()
                {
                    StatusCode = statusCode,
                    ErrorCode = statusCodeException?.ErrorCode ?? 0,
                    Path = exceptionHandlerFeature.Path,
                    RouteValues = exceptionHandlerFeature?.RouteValues?.ToDictionary(),
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                };

                return Results.Json(errorMessage, statusCode: statusCode);
            });

            return webApplication;
        }
    }
}
