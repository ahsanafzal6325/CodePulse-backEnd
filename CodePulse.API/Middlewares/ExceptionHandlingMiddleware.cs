using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient; 

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.ContentType = "application/json";

            int statusCode;
            object response;

            switch (ex)
            {
                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        StatusCode = statusCode,
                        Message = "Unauthorized access.",
                        Detailed = ex.Message
                    };
                    break;

                case ValidationException validationEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        StatusCode = statusCode,
                        Message = "Validation failed.",
                        Errors = validationEx.ValidationResult?.ErrorMessage
                    };
                    break;

                case DbUpdateException dbEx:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        StatusCode = statusCode,
                        Message = "Database update failed.",
                        Detailed = dbEx.InnerException?.Message ?? dbEx.Message
                    };
                    break;

                case SqlException sqlEx:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        StatusCode = statusCode,
                        Message = "A database error occurred.",
                        SqlErrorNumber = sqlEx.Number,
                        Detailed = sqlEx.Message
                    };
                    break;

                case ArgumentNullException argNullEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        StatusCode = statusCode,
                        Message = "A required argument was null.",
                        Detailed = argNullEx.Message
                    };
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        StatusCode = statusCode,
                        Message = "An unexpected error occurred.",
                        Detailed = ex.Message
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
