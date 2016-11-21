using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class REventCalendar
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string AVAILABLEFROM { get; set; }
        public string AVAILABLETO { get; set; }
        public float ATTENDEECOST { get; set; }
    }
}