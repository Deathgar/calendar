using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace MVC5FullCalandarPlugin.Models
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public JwtSecurityToken Token { get; set; }
        public List<PublicHoliday> PublicHolidays { get; set; }
    }
}