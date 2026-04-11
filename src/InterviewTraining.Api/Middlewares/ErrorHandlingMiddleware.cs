using InterviewTraining.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Middlewares;

/// <summary>
/// Логирование необработанных контроллерами исключений
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private static readonly Newtonsoft.Json.JsonSerializerSettings _settings = new()
    {
        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = false
            }
        }
    };

/// <summary>
/// Конструктор для создания c DI
/// </summary>
/// <param name="next"></param>
public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    /// <summary>
    /// Обертка с перехватом исключений из следующего обработчика
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
    {
        context.Response.ContentType = "application/json";
        context.Response.Headers.TryAdd("Access-Control-Allow-Origin", "*");

        // Контролируемые ошибки
        if (exception is EntityNotFoundException entityNotFoundException)
        {
            var errorId = Guid.NewGuid();
            var errorMessage = $"Entity not found. Error #{errorId}";
            logger.LogError(exception, errorMessage);
            logger.LogError($"EntityNotFoundException: {entityNotFoundException.Message}. Exception as JSON: {Serialize(entityNotFoundException)}");
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                StatusCode = StatusCodes.Status404NotFound,
                Error = entityNotFoundException
            });
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(result, encoding: Encoding.UTF8);
        }
        else if (exception is BusinessLogicException businessLogicException)
        {
            logger.LogError($"BusinessLogicException: {exception.Message}. Exception as JSON: {Serialize(businessLogicException)}");
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Error = businessLogicException
            });
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(result, encoding: Encoding.UTF8);
        }
        else
        {
            logger.LogError($"Exception: {exception.Message}. Exception as JSON: {Serialize(exception)}");
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = exception
            });
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(result, encoding: Encoding.UTF8);
        }
    }

    private static string Serialize(EntityNotFoundException ex)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(ex, _settings);
    }

    private static string Serialize(BusinessLogicException ex)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(ex, _settings);
    }

    private static string Serialize(Exception ex)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(ex, _settings);
    }
}
