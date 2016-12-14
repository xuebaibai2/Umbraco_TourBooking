﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class EventCalendar
    {
        public EventCalendar()
        {
            this.IsSameContact = false;
            AdditionalInfo = new AdditionalInfoModel();
            Invoice = new Invoice();
            GroupCoordinator = new GroupCoordinator();
        }
        
        public string id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool IsSameContact { get; set; }
        public bool IsInvoiceOnly { get; set; }
        public GroupCoordinator GroupCoordinator { get; set; }
        public Invoice Invoice { get; set; }
        public AdditionalInfoModel AdditionalInfo{ get; set; }

        public string GetDateDescription()
        {
            var start_time = Convert.ToDateTime(start);
            var end_time = Convert.ToDateTime(end);
            return start_time.ToString("dddd dd MMMM") + " from " + start_time.ToString("htt") + " to " + end_time.ToString("htt");
                
        }
    }
}