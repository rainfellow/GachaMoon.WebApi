using GachaMoon.Common.Exceptions.Login;
using GachaMoon.Common.Exceptions.Users;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GachaMoon.WebApi.Middlewares;

#pragma warning disable IDE0051
/// <inheritdoc />
public class CustomProblemDetailsFactory(ILogger<CustomProblemDetailsFactory> logger) : ProblemDetailsFactory
{
    private const string BaseStatusCodeType = "https://httpstatuses.com/";

    private static readonly IReadOnlyDictionary<Type, Func<CustomProblemDetailsFactory, Func<Exception, ProblemDetails?>>> ExceptionHandlers = new Dictionary<Type, Func<CustomProblemDetailsFactory, Func<Exception, ProblemDetails?>>>
    {
        { typeof(ValidationException), x => x.ValidationExceptionHandler},
        { typeof(UserAlreadyExistsException), x => x.ConflictExceptionHandler},
        { typeof(UserNotFoundException), x => x.NotFoundExceptionHandler},
        { typeof(LoginFailedException), x => x.BadRequestExceptionHandler},
        { typeof(MissingIdentityException), x => x.NotAuthorizedExceptionHandler},
        { typeof(WrongPasswordException), x => x.BadRequestExceptionHandler},
        { typeof(ArgumentException), x => x.NotFoundExceptionHandler},
    };

    private readonly ILogger<CustomProblemDetailsFactory> _logger = logger;

    /// <inheritdoc />
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= StatusCodes.Status500InternalServerError;

        ProblemDetails? problemDetails = null;
        var context = httpContext.Features.Get<IExceptionHandlerFeature>();

        if (context?.Error != null)
        {
            var exceptionType = context.Error.GetType();
            if (ExceptionHandlers.TryGetValue(exceptionType, out var handler) ||
                (exceptionType.BaseType != null && ExceptionHandlers.TryGetValue(exceptionType.BaseType, out handler)))
            {
                problemDetails = handler(this)(context.Error);
                if (problemDetails?.Status != null)
                {
                    statusCode = problemDetails.Status;
                }
            }
        }

        problemDetails ??= UnknownExceptionHandler(context?.Error);

        ApplyProblemDetailsData(problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <inheritdoc />
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= StatusCodes.Status400BadRequest;

        ValidationProblemDetails problemDetails = new(modelStateDictionary);

        ApplyProblemDetailsData(problemDetails, statusCode.Value);

        return problemDetails;
    }

    private ProblemDetails UnknownExceptionHandler(Exception? e)
    {
        if (e != null)
        {
            _logger.LogError(e, "An unhandled exception has occurred while executing the request.");
        }

        return new ProblemDetails
        {
            Title = "An error occurred while processing your request.",
        };
    }

    private ProblemDetails? ValidationExceptionHandler(Exception e)
    {
        if (e is not ValidationException exception)
        {
            return null;
        }

        _logger.LogWarning(exception, "{ExceptionMessage}", exception.Message);

        return new ValidationProblemDetails(ValidationErrorToDictionary(exception))
        {
            Status = StatusCodes.Status400BadRequest
        };
    }

    private ProblemDetails NotFoundExceptionHandler(Exception e)
    {
        _logger.LogWarning(e, "{ExceptionMessage}", e.Message);

        return new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = $"Resource was not found. {e.Message}"
        };
    }

    private ProblemDetails ConflictExceptionHandler(Exception e)
    {
        _logger.LogError(e, "{ExceptionMessage}", e.Message);

        return new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = $"Conflict: {e.Message}"
        };
    }

    private ProblemDetails MethodNotAllowedExceptionHandler(Exception e)
    {
        _logger.LogError(e, "{ExceptionMessage}", e.Message);

        return new ProblemDetails
        {
            Status = StatusCodes.Status405MethodNotAllowed,
            Title = e.Message
        };
    }

    private ProblemDetails BadRequestExceptionHandler(Exception e)
    {
        _logger.LogWarning(e, "{ExceptionMessage}", e.Message);

        return new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = e.Message
        };
    }

    private ProblemDetails? GenericExceptionHandler<T>(Exception e) where T : Exception
    {
        if (e is not T exception)
        {
            return null;
        }

        ProblemDetails problemDetails = new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = exception.Message
        };

        _logger.LogError(exception, "{ExceptionMessage}", exception.Message);

        return problemDetails;
    }

    private ProblemDetails NotAuthorizedExceptionHandler(Exception e)
    {
        _logger.LogError(e, "{ExceptionMessage}", e.Message);

        return new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Detail = "Unauthorized"
        };
    }

    private ProblemDetails ForbiddenExceptionHandler(Exception e)
    {
        _logger.LogError(e, "{ExceptionMessage}", e.Message);

        return new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Detail = "Forbidden"
        };
    }

    private static void ApplyProblemDetailsData(ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;
        problemDetails.Type ??= $"{BaseStatusCodeType}{statusCode}";
    }

    private static IDictionary<string, string[]> ValidationErrorToDictionary(ValidationException exception)
    {
        return exception.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
