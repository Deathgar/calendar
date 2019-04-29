using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MVC5FullCalandarPlugin.Models;

namespace MVC5FullCalandarPlugin.Services.Interfaces
{
    public interface IDayEvent
    {
        string ChangeTimeAndEvent(string title, string description, string time, string date, string token,
            string id, HttpPostedFileBase image);

        string AddTimeAndEvent(string title, string description, string time, string date, string token,
            HttpPostedFileBase image);

        string GetAllTimeInDay(string date, string token);

        PublicHoliday GetEvent(string token, string date, string id);

        string Delete(string id, string date, string token);

    }
}
