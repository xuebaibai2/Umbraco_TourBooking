using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models.API
{
    public class API_TOURBOOKINGATTENDEESUMMARY
    {
        public string ID { get; set; }
        public string TOURID { get; set; }
        public string TOURBOOKINGID { get; set; }
        public string ATTENDEETYPEID { get; set; }
        public int QUANTITYBOOKED { get; set; }
        public int QUANTITYATTENDED { get; set; }
        public float ATTENDEECOST { get; set; }
        public float DISCOUNT { get; set; }
        public float FINALCOST { get; set; }
    }
}