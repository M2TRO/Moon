using Core.Domain.Database;
using Core.Interfaces;

namespace WarpPortalAPI.Service
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
      
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IDatabeseService userService, IJwtUtils jwtUtils)
        {



            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
       
            if (userId != null)
            {
            //    context.Items["User"] = userId.Value;
                // attach user to context on successful jwt validation
             
               context.Items["User"] = userService.GetAccountbyId(userId.Value).Result;
            }

            await _next(context);
        }
    }
}
