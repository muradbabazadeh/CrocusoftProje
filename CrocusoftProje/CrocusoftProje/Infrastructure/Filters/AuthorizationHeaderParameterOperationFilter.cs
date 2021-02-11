using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace CrocusoftProje.Api.Infrastructure.Filters
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiOperation operation, OperationFilterContext context)
        {
            var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors.Select(filterInfo => filterInfo.Filter).ToList();

            var isAuthorized = filters.Any(filter => filter is Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter);

            var allowAnonymous = filters.Any(filter => filter is Microsoft.AspNetCore.Mvc.Authorization.IAllowAnonymousFilter);

            if (!isAuthorized || allowAnonymous) return;

            if (operation.Parameters == null)
                operation.Parameters = new List<Microsoft.OpenApi.Models.OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Access token",
                Required = true,
            });
        }
    }
}
