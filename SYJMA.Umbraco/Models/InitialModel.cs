using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYJMA.Umbraco.Models
{
    public class InitialModel
    {
        public int Id { get; set; }
        public string PreferredDate { get; set; }
        public string Comments { get; set; }
        public string Program { get; set; }
        public string type { get; set; }
        public EventCalendar Event { get; set; }
        public IEnumerable<SelectListItem> ProgramList { get; set; }
        public IEnumerable<EventCalendar> EventList { get; set; }
    }
}