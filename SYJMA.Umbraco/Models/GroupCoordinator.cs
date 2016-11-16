using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class GroupCoordinator
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SureName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string DaytimeNumber { get; set; }
        public Invoice Invoice { get; set; }
    }
}