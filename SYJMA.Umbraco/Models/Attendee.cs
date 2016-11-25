using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class Attendee 
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public float Cost { get; set; }
        //public int Number { get; set; }
    }
}