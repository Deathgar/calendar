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
        public void AddImage()
        {
            var wd = Request.Files["id"];
            UploadImage(new PublicHoliday(), Request.Files[0]).Wait();
        }

        [HttpPost]
        public string AddTimeAndEvent(string title, string description, string time, string date, string token, HttpPostedFileBase image)
        {
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
                UploadImage(eventHoliday, image).Wait();
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

        private async Task<string> UploadImage(PublicHoliday holy, HttpPostedFileBase imagePath)
        {
            var id = DateTime.Now.GetHashCode() + "";

            var path = @"D:/Files/" + imagePath.FileName;

            imagePath.SaveAs(path);

            var stream = System.IO.File.Open(path, FileMode.Open);

            var task = new FirebaseStorage("testcalendar-27287.appspot.com")
                .Child(id)
                .PutAsync(stream);


             var f = await task;

             holy.Image.Id = id;
             holy.Image.Url = f;

             return f;
        }

        [HttpPost]
        public string ChangeTimeAndEvent(string title, string description, string time, string date, string token, string id, HttpPostedFileBase image)
        {
           
            
            var email = TokenService.getEmailWithToken(token);
            var user = storageUsers.Get(email);
            var dat = user.Days.First(x => x.Date == date);
            var holy = dat.PublicHolidays.First(x => x.Id == id);

            holy.Time = time;
            holy.Description = description;
            holy.Title = title;

            if (image != null)
            {
                UploadImage(holy, image).Wait();
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
            day.PublicHolidays.Remove(day.PublicHolidays.First(x => x.Id == id));

            storageUsers.Update(user);

            return id;
        }
    }
}