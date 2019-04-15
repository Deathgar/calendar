using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using MVC5FullCalandarPlugin.FireBase;
using MVC5FullCalandarPlugin.FireBase.Users;
using MVC5FullCalandarPlugin.Models;

namespace MVC5FullCalandarPlugin.Controllers
{
    public class DayEventsController : Controller
    {
        [HttpPost]
        public string SetTimeAndEvent(string events, string hour, string date, string token)
        {
            var userService = new UserService();

            var email = TokenService.getEmailWithToken(token);
            var user = Storage.getInstance().GetUser(email);

            var startDate = date + " " + "00:00";

            var eventHoliday = new PublicHoliday
            {
                Description = events,
                Start_Date = startDate,
                End_Date = date,
                Time = hour,
                Title = events
            };

            user.PublicHolidays.Add(eventHoliday);
            userService.UpdateUser(user);
           //SessionStateMode.
           //var z = Session["hhhhhhh@mail.ru"];
         //  Session["hhhhhhh@mail.ru"] = "eee";
           //HttpContext.Response.Cookies["hhhhhhh@mail.ru"].Value = "hz";
           //var p = HttpContext.Request.Cookies["hhhhhhh@mail.ru"];
           //bool w = a.Login("hhhhhhh@mail.ru", "vlad");


            //using (StreamWriter sw = new StreamWriter("C:\\Users\\user\\Desktop\\MVC5FullCalandarPlugin\\MVC5FullCalandarPlugin\\Content\\files\\PublicHoliday.txt", true, System.Text.Encoding.Default))
            //{
            //    var s = "0," + events + "," + events + "," + date + " 09:00," + date;
            //    sw.WriteLine(s);

            //    sw.Dispose();
            //    sw.Close();
            //}
            return "ww";
        }
    }
}