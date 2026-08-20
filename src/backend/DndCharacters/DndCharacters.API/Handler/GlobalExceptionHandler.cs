using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DndCharacters.API.Handler;

internal sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = exception switch
        {
            ValidationException validationException =>
                CreateValidationProblemDetails(validationException),

            KeyNotFoundException =>
                CreateProblemDetails(
                    StatusCodes.Status404NotFound,
                    "Resource not found",
                    exception.Message),

            InvalidOperationException =>
                CreateProblemDetails(
                    StatusCodes.Status409Conflict,
                    "Conflict",
                    exception.Message),

            ArgumentException =>
                CreateProblemDetails(
                    StatusCodes.Status400BadRequest,
                    "Bad request",
                    exception.Message),

            _ =>
                CreateProblemDetails(
                    StatusCodes.Status500InternalServerError,
                    "Internal server error",
                    "An unexpected error occurred.")
        };

        LogError(httpContext, exception, problemDetails);

        problemDetails.Instance = httpContext.Request.Path;
        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        httpContext.Response.StatusCode =
            problemDetails.Status ?? StatusCodes.Status500InternalServerError;


        await problemDetailsService.WriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
                Exception = exception
            });

        return true;
    }

    private void LogError(HttpContext httpContext, Exception exception, ProblemDetails problemDetails)
    {
        if (problemDetails.Status >= 500)
        {
            logger.LogError(
                exception,
                "Unhandled exception occurred while processing {Method} {Path}",
                httpContext.Request.Method,
                httpContext.Request.Path);
        }
        else
        {
            logger.LogWarning(
                exception,
                "Request failed with status {StatusCode}: {Method} {Path}",
                problemDetails.Status,
                httpContext.Request.Method,
                httpContext.Request.Path);
        }
    }

    private static ProblemDetails CreateProblemDetails(
        int statusCode,
        string title,
        string detail)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail
        };
    }

    private static ValidationProblemDetails CreateValidationProblemDetails(
        ValidationException exception)
    {
        Dictionary<string, string[]> errors = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(x => x.ErrorMessage)
                    .Distinct()
                    .ToArray());

        return new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation failed",
            Detail = "One or more validation errors occurred."
        };
    }
}