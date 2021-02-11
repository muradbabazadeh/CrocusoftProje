using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrocusoftProje.Identity.Auth;
using CrocusoftProje.Identity.Queries;
using CrocusoftProje.Identity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CrocusoftProje.Api.Infrastructure.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizePermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string[] Permissions { get; set; }

        private IUserQueries _userQueries;

        private IUserManager _userManager;

        public AuthorizePermissionAttribute(params string[] codes)
        {
            Permissions = codes;
        }

        public void OnAuthorization(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
            }

            _userQueries = (IUserQueries)context.HttpContext.RequestServices.GetService(typeof(IUserQueries));
            _userManager = (IUserManager)context.HttpContext.RequestServices.GetService(typeof(IUserManager));

            IDictionary<string, PermissionDTO> permissions = _userQueries.GetPermissionsAsync(_userManager.GetCurrentUserId()).GetAwaiter().GetResult();
            bool isAuthorized = !Permissions.Any() || (permissions != null && Permissions.All(p => permissions.ContainsKey(p)));

            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
