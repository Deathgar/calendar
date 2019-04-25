using System.Collections.Generic;

namespace MVC5FullCalandarPlugin.Models
{
    public class EventsWithEqualsName
    {
        public string Id { get; set; }
        public List<string> Dates { get; set; }
        public List<string> Times { get; set; }
    }
}