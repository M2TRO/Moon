using Core.Domain.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Core.Models;

namespace WarpPortalAPI.Service
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthzAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;


        //    var token = context.HttpContext.Request.Headers["Authorization"];
            // authorization
            var user = (TblAccount)context.HttpContext.Items["User"];


            if (user == null)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };



        }
    }
}
