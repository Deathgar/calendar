using MVC5FullCalandarPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private IUserDbSet storageUsers;
        public IDayEvent dayEvents;

        public HomeController(IUserDbSet storage, IDayEvent dayEvent)
        {
            storageUsers = storage;
            dayEvents = dayEvent;
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

        [HttpPost]
        public string ChangeTimeAndEvent()
        {
            var image = Request.Files["img"];
            var title = Request.Form["title"];
            var description = Request.Form["description"];
            var time = Request.Form["time"];
            var date = Request.Form["date"];
            var token = Request.Form["token"];
            var id = Request.Form["id"];
            var status = Request.Form["status"];

            return dayEvents.ChangeTimeAndEvent(title, description, time, date, token, id,status, image);
        }

        [HttpPost]
        public string AddTimeAndEvent()
        {
            var image = Request.Files["img"];
            var title = Request.Form["title"];
            var description = Request.Form["description"];
            var time = Request.Form["time"];
            var date = Request.Form["date"];
            var token = Request.Form["token"];
            var status = Request.Form["status"];

            return dayEvents.AddTimeAndEvent(title, description, time, date, token, status,image);
        }

        [HttpGet]
        public string GetAllTimeInDay(string date, string token)
        {
            return dayEvents.GetAllTimeInDay(date, token);
        }

        [HttpGet]
        public ActionResult GetEvent(string token, string date, string id)
        {
            if (token != "null" && token != null)
            {
                var holy = dayEvents.GetEvent(token, date, id);
                JsonResult result = new JsonResult();
                
                

                try
                {
                    result = this.Json(holy, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }

                // Return info.
                return result;
            }

            return null;
        }

        [HttpPost]
        public string Delete(string id, string date, string token)
        {
            return dayEvents.Delete(id, date, token);
        }


    }
}