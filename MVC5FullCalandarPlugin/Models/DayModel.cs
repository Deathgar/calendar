using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5FullCalandarPlugin.Models
{
    public class DayModel
    {
        public string Date { get; set; }
        public List<PublicHoliday> PublicHolidays { get; set; }

        public string AllTime
        {
            get
            {
                int sum = PublicHolidays.Sum(x => Int32.Parse(x.Time));
                return sum.ToString();
            }
        }
    }
}