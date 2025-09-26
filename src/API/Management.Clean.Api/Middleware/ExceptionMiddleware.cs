using System.Net;
using Management.Clean.Api.Middleware.Models;
using Management.Clean.Application.Exceptions;
using Newtonsoft.Json;

namespace Management.Clean.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem;

        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(badRequestException),
                    Errors = badRequestException.ValidationErrors
                };

                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(notFoundException)
                };

                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Detail = ex.StackTrace,
                    Type = nameof(HttpStatusCode.InternalServerError)
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        var logMessage = JsonConvert.SerializeObject(problem);
        _logger.LogError(logMessage);
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}