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

        public AllEventsController(UserDbSet s)
        {
            storage = s;
        }
        // GET: AllEvents
        public ActionResult Index()
        {
            

            return View();
        }
    }
}