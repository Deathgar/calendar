using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5FullCalandarPlugin.Models;
using MVC5FullCalandarPlugin.Services;
using MVC5FullCalandarPlugin.Services.Users;

namespace MVC5FullCalandarPlugin.Controllers
{
    public class DiagramsController : Controller
    {
        // GET: Diagrams
        public ActionResult Index()
        {
           

            return View();
        }

        [HttpGet]
        public void GetN(string token)
        {
            var model = new SpecialEventModel();

            var storage = new Storage();
            var email = TokenService.getEmailWithToken(token);
            var user = storage.Get(email);
            
            var namesEvent = new List<string>();

            user.Days.ForEach(
                x => x.PublicHolidays.ForEach(
                    e => namesEvent.Add(e.Title)));

            var arr = namesEvent.Distinct();

            List<DayModel> days;

            var w = new List<EventsWithEqualsName>();


            foreach (var title in arr)
            {
                days = user.Days.FindAll(x => x.PublicHolidays.Any(e => e.Title == title)).ToList();
                
                var daysN = new List<string>();
                var times = new List<string>();

                days.ForEach(x => daysN.Add(x.PublicHolidays.Find(e => e.Title == title).End_Date));
                days.ForEach(x => times.Add(x.PublicHolidays.Find(e => e.Title == title).Time));

                w.Add(new EventsWithEqualsName
                {
                    Title = title,
                    Times = times,
                    Dates = daysN

                });
            }

            model.Events = w;

            return;
        }
    }
}