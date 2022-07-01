using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace DevTubeCommerce.Framework.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, bool isDevelopment = false)
        {
            bool runningInTheIDE = isDevelopment && Debugger.IsAttached;

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null)
                        return;

                    (HttpStatusCode statusCode, Error error) = contextFeature.Error switch
                    {
                        null => (HttpStatusCode.InternalServerError, null),
                        NotFoundException ex => (HttpStatusCode.NotFound, ex.Error),
                        BusinessRuleException ex => (HttpStatusCode.BadRequest, ex.Error),
                        InvalidStateException ex => (HttpStatusCode.BadRequest, ex.Error),
                        AppException ex => (HttpStatusCode.InternalServerError, ex.Error),
                        _ => (HttpStatusCode.InternalServerError, null)
                    };

                    var errorModel = new ErrorModel
                    {
                        Code = error?.Id ?? 0,
                        StatusCode = statusCode,
                        Description = error?.PersianTitle,
                        StackTrace = isDevelopment ? contextFeature.Error?.ToString() : null
                    };
                    var responseModel = ResponseModel.FromError(errorModel);

                    context.Response.ContentType = "application/json; charset=utf-8";
                    context.Response.StatusCode = (int)statusCode;
                    var option = new JsonSerializerOptions()
                    {
                        IgnoreReadOnlyProperties = false,
                        PropertyNameCaseInsensitive = true,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    };

                    await JsonSerializer.SerializeAsync(context.Response.Body, responseModel, option);

                    if (runningInTheIDE)
                    {
                        await context.Response.CompleteAsync();
                    }
                });
            });
        }
    }
}
