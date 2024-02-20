using Core.Domain.Database;
using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

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


                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;

                var exp = tokenS.ValidTo > DateTime.UtcNow;
                if (exp)
                {
                    var type = tokenS.Claims.First(x => x.Type == "type").Value;
                    switch (type)
                    {
                        case "1":

                            context.Items["User"] = userService.GetAccountbyId(userId.Value).Result;

                            break;
                        case "2":
                            var verify = JsonConvert.DeserializeObject<MdlGenVer>(tokenS.Claims.First(x => x.Type == "VerToken").Value);
                            context.Items["verify"] = verify;
                            context.Items["User"] = userService.GetAccountbyId(userId.Value).Result;
                            break;

                    }

                }


            }

            await _next(context);
        }
    }
}
