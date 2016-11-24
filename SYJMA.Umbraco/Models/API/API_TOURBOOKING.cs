using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models.API
{
    public class API_TOURBOOKING
    {
        public string ID { get; set; }
        public string REFERENCE { get; set; }
        public string TOURID { get; set; }
        public string STARTDATE { get; set; }
        public string STARTTIME { get; set; }
        public string ENDDATE { get; set; }
        public string ENDTIME { get; set; }
        public string STATUS { get; set; }
        public string BOOKERSERIALNUMBER { get; set; }
        public string FORSERIALNUMBER { get; set; }
        public string INVOICEESERIALNUMBER { get; set; }
        public string YEARGROUP { get; set; }
        public string SUBJECT { get; set; }
        public float TOTALCOST { get; set; }
        public float TOTALPAID { get; set; }
        public string BOOKINGCOMMENT { get; set; }
        public string OWNERSERIALNUMBER { get; set; }
    }
}