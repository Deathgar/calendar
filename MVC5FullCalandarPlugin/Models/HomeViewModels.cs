using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC5FullCalandarPlugin.Models
{
    public class PublicHoliday
    {
        public string Id { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
    }
}
