using MVC5FullCalandarPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using MVC5FullCalandarPlugin.Services;
using MVC5FullCalandarPlugin.Services.Interfaces;
using MVC5FullCalandarPlugin.Services.Users;
using Ninject;


namespace MVC5FullCalandarPlugin.Controllers
{
    public class HomeController : Controller
    {
        #region Index method

        private IUserDbSet storageUsers;

        public HomeController(IUserDbSet storage)
        {
            storageUsers = storage;
        }

        /// <summary>
        /// GET: Home/Index method.
        /// </summary>
        /// <returns>Returns - index view page</returns> 
        public ActionResult Index()
        {
            // Info.
            return View();
        }

        #endregion

        #region Get Calendar data method.


        /// <summary>
        /// GET: /Home/GetCalendarData
        /// </summary>
        /// <returns>Return data</returns>
        [HttpGet]
        public ActionResult GetCalendarData(string token)
        {

            if (token != "null" && !string.IsNullOrEmpty(token))
            {
                var email = TokenService.getEmailWithToken(token);

                // Initialization.
                JsonResult result = new JsonResult();

                try
                {
                    //dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(rawJsonStr);
                    // Loading.
                    List<DayModel> data = storageUsers.Get(email).Days;


                    data?.RemoveAll(x => x == null);
                    // Processing.
                    result = this.Json(data, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    // Info
                    Console.Write(ex);
                }

                // Return info.
                return result;
            }

            return null;
        }

        #endregion

    }
}