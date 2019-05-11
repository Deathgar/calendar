using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5FullCalandarPlugin.Services;
using MVC5FullCalandarPlugin.Services.Interfaces;
using MVC5FullCalandarPlugin.Services.Users;

namespace MVC5FullCalandarPlugin.Controllers
{
    public class AllEventsController : Controller
    {
        private IUserDbSet storage;
        private IDayEvent dayEvents;

        public AllEventsController(IDayEvent dayEvent , UserDbSet s)
        {
            storage = s;
            dayEvents = dayEvent;
        }
        // GET: AllEvents
        public ActionResult Index()
        {
            var users = storage.GetAll();

            return View(users);
        }

        [HttpPost]
        public ActionResult ChangeTimeAndEvent()
        {
            var image = Request.Files["img"];
            var title = Request.Form["title"];
            var description = Request.Form["description"];
            var time = Request.Form["time"];
            var date = Request.Form["date"];
            var email = Request.Form["email"];
            var id = Request.Form["id"];
            var status = Request.Form["status"];

            var url = dayEvents.ChangeTimeAndEventWithEmail(title, description, time, date, email, id, status, image);
            return Json(url);
        }
    }
}