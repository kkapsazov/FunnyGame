using System.Net;
using System.Text.Json;
using FunnyGame.Application.Dtos;
using FunnyGame.Application.Exceptions;

namespace FunnyGame.Api.Utils;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex);
        }
    }

    private Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        ErrorResponse response = new();

        if (exception is AppException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            response.Error = exception.Message;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Error = "Internal Server Error";
        }

        logger.LogError(exception, exception.Message);
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
