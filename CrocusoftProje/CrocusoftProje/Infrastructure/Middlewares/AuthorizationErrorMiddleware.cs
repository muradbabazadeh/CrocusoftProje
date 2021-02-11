using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace CrocusoftProje.Api.Infrastructure.Middlewares
{
    internal class AuthorizationErrorMiddleware
    {
        private readonly Microsoft.AspNetCore.Http.RequestDelegate _next;

        public AuthorizationErrorMiddleware(Microsoft.AspNetCore.Http.RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            await _next(context);
            var statusCode = context.Response.StatusCode;
            if ((statusCode == 403 || statusCode == 401) && !context.Response.HasStarted)
            {
                var exceptionName = statusCode == 401 ? nameof(System.Security.Authentication.AuthenticationException) : nameof(UnauthorizedAccessException);
                var message = statusCode == 401 ? "Unauthorized" : "Forbidden";
                var response =
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        new ErrorHandling.JsonErrorResponse(exceptionName, message, null), new Newtonsoft.Json.JsonSerializerSettings
                        {
                            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                            Formatting = Formatting.Indented
                        });
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(response);
            }
        }
    }
}
