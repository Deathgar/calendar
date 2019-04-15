using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace MVC5FullCalandarPlugin.FireBase
{
    public class TokenService
    {
        public static string getEmailWithToken(string token)
        {
            var newToken = new JwtSecurityToken(token);
            var f = newToken.Claims.First(x => x.Type.Equals("email"));

            return f.Value;
        }
    }
}