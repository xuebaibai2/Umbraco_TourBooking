using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class EventCalendarWrapper
    {
        public EventCalendar EventCalendar { get; set; }
    }

    public class EventCalendar
    {
        public string id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}