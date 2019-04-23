using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Storage;
using MVC5FullCalandarPlugin.Models;
using MVC5FullCalandarPlugin.Services;
using MVC5FullCalandarPlugin.Services.Interfaces;
using MVC5FullCalandarPlugin.Services.Users;
using Ninject;
using Ninject.Activation;


namespace MVC5FullCalandarPlugin.Controllers
{
    public class DayEventsController : Controller
    {
        private IUserDbSet storageUsers;

        public DayEventsController(IUserDbSet storage)
        {
            storageUsers = storage;
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

            var email = TokenService.getEmailWithToken(token);
            var user = storageUsers.Get(email);

            var startDate = date + " " + "00:00";
            var id = DateTime.Now.GetHashCode().ToString();

            var eventHoliday = new PublicHoliday
            {
                Id = id,
                Description = description,
                Start_Date = startDate,
                End_Date = date,
                Time = time,
                Title = title
            };

            if (image != null)
            {

                var task = Task.Run(async () => { await FireBaseStorage.UploadImage(eventHoliday, image); });
                task.Wait();
            }

            if (user.Days == null)
            {
                user.Days = new List<DayModel>();
            }

            if (user.Days.Any(x => x.Date == date))
            {
                user.Days.First(x => x.Date == date).PublicHolidays.Add(eventHoliday);
            }
            else
            {
                user.Days.Add(new DayModel()
                {
                    Date = date,
                    PublicHolidays = new List<PublicHoliday>()
                    {
                        eventHoliday
                    }
                });
            }

            storageUsers.Update(user);
      
            return id;
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

            var email = TokenService.getEmailWithToken(token);
            var user = storageUsers.Get(email);
            var dat = user.Days.First(x => x.Date == date);
            var holy = dat.PublicHolidays.First(x => x.Id == id);

            holy.Time = time;
            holy.Description = description;
            holy.Title = title;

            if (image != null )
            {
                if (holy.Image != null)
                {
                    var task = Task.Run(async () => { await FireBaseStorage.UploadImage(holy, image, holy.Image.Id); });
                    task.Wait();
                }
                else
                {
                    var task = Task.Run(async () => { await FireBaseStorage.UploadImage(holy, image); });
                    task.Wait();
                }
            }

            storageUsers.Update(user);

            return id;
        }

        [HttpGet]
        public string GetAllTimeInDay(string date, string token)
        {
            var user = storageUsers.Get(TokenService.getEmailWithToken(token));
            string str = user.Days.FirstOrDefault(x => x.Date == date).AllTime;
            return str;
        }

        public ActionResult GetEvent(string token, string date, string id)
        {
            if (token != "null" && token != null)
            {
                var email = TokenService.getEmailWithToken(token);

                // Initialization.
                JsonResult result = new JsonResult();

                try
                {
                    //dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(rawJsonStr);
                    // Loading.
                    List<DayModel> data = storageUsers.Get(email).Days;
                    var day = data.First(x => x.Date == date);
                    var holy = day.PublicHolidays.First(x => x.Id == id);

                    // Processing.
                    result = this.Json(holy, JsonRequestBehavior.AllowGet);
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
        public string Delete(string id, string date ,string token)
        {
            var email = TokenService.getEmailWithToken(token);
            var user = storageUsers.Get(email);
            var day = user.Days.First(x => x.Date == date);
            var holy = day.PublicHolidays.First(x => x.Id == id);
            FireBaseStorage.DeleteImage(holy.Image.Id);
            day.PublicHolidays.Remove(holy);

            storageUsers.Update(user);

            return id;
        }
    }
}