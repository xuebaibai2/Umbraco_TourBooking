﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Utility;

namespace SYJMA.Umbraco.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string PreferredDate { get; set; }
        public string Comments { get; set; }
        public string Program { get; set; }
        public string type { get; set; }
        public string TourBookingID { get; set; }
        public List<Attendee> AttendeeList = new List<Attendee>();
        public EventCalendar Event { get; set; }
        public IEnumerable<SelectListItem> ProgramList { get; set; }
        public IEnumerable<EventCalendar> EventList { get; set; }
    }
}