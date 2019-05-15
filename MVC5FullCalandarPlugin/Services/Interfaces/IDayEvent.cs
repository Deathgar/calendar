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
            string id, string status, HttpPostedFileBase image);

        string ChangeTimeAndEventWithEmail(string title, string description, string time, string date, string email,
            string id, string status, HttpPostedFileBase image);

        string AddTimeAndEvent(string title, string description, string time, string date, string token, string status,
            HttpPostedFileBase image);

        PublicHoliday AddTimeAndEventWithEmail(string title, string description, string time, string date, string email, string status,
            HttpPostedFileBase image);

        string GetAllTimeInDay(string date, string token);

        PublicHoliday GetEvent(string token, string date, string id);

        Task<string> Delete(string id, string date, string token);
        void DeleteWithEmail(string id, string date, string email);

    }
}
