using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MVC5FullCalandarPlugin.Services.Interfaces;
using MVC5FullCalandarPlugin.Services.Users;
using Ninject;


namespace MVC5FullCalandarPlugin.Controllers
{
    public class AuthenticationController : Controller
    {
        private const int lenghSalt = 8;
        private const string cookieName = "authCoockie";

        private IAuthenication authenicationService;

        public AuthenticationController(IAuthenication auth)
        {
            authenicationService = auth;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Registration(string email, string password, string firstName)
        {
            return authenicationService.Registration(email, password, firstName);
        }

        [HttpPost]
        public string Login(string email, string password)
        {
            var token = authenicationService.Login(email, password);
          
            return token;
        }

        [HttpPost]
        public void LogOut(string token)
        {
            authenicationService.LogOut(token);
        }
    }
}