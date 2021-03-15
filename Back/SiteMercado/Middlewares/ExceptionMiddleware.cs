using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SiteMercado.Api
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = default(HttpStatusCode);

            object message = null;

            switch (exception)
            {
                case SecurityException security:    
                    statusCode = HttpStatusCode.Forbidden;

                    message = "Sem acesso!";

                    _logger.LogError($"EXCEPTION HANDLING 403 | { security.Message }");
                    break;
                case ArgumentNullException argumentNull:
                    statusCode = HttpStatusCode.NotFound;

                    message = !string.IsNullOrEmpty(argumentNull.Message) ? argumentNull.Message : "Não encontrado";

                    _logger.LogError($"EXCEPTION HANDLING 404 | { argumentNull.Message }");
                    break;                
                case ArgumentOutOfRangeException argumentOutOfRange:
                    statusCode = HttpStatusCode.BadRequest;

                    message = !string.IsNullOrEmpty(argumentOutOfRange.Message) ? argumentOutOfRange.Message : "Valor incorreto";

                    _logger.LogError($"EXCEPTION HANDLING 400 | { argumentOutOfRange.Message }");
                    break;
                case ValidationException validation:
                    statusCode = HttpStatusCode.BadRequest;
                    List<object> faults = new List<object>();

                    foreach (var erro in validation.Errors)
                    {
                        var fault = new
                        {                           
                            error = erro.ErrorMessage,
                            property = erro.PropertyName,
                            value = erro.AttemptedValue == null ? "null" : erro.AttemptedValue.ToString()
                        };

                        faults.Add(fault);
                    }

                    message = faults;

                    _logger.LogError($"EXCEPTION HANDLING 400 | { validation.Message }");
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                                       
                    message = "Erro interno";

                    _logger.LogError($"EXCEPTION HANDLING 500 | { exception }");
                    break;
            }

            var result = JsonConvert.SerializeObject(message);

            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(result, Encoding.UTF8);
        }
    }
}
