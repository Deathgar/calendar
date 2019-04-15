using System;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MVC5FullCalandarPlugin.FireBase;
using MVC5FullCalandarPlugin.FireBase.Users;
using MVC5FullCalandarPlugin.Models;


namespace MVC5FullCalandarPlugin.Controllers
{
    public class AuthenticationController : Controller
    {
        private const int lenghSalt = 8;
        private const string cookieName = "authCoockie";


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Registration(string email, string password, string firstName)
        {
            return Authenication.Registration(email, password, firstName);

        }

        [HttpPost]
        public string Login(string email, string password)
        {
            var token = Authenication.Login(email, password);
           // var crud = new UserCrud();
            //User user = crud.GetUser(email);
            //var success = user.HashPassword.Equals(Security.GetHash(password));

            //if (success)
            //{
            //    //Session["auth"] = sessionId;
            //    //string cookie = Security.GetHash(Security.GenerateSalt(10) + user.Salt);

            //    //user.Cookie = cookie;
            //    //HttpContext.Response.Cookies[cookieName].Value = cookie;
            //}
            
            //return success;
            return token;
        }

        [HttpPost]
        public void LogOut(string token)
        {
            Authenication.LogOut(token);
        }

        [HttpPost]
        public bool IsAuth()
        {
            return true;
            /*  if (Session[sessionName]?)
            {

            }
            */
        }
    }
}