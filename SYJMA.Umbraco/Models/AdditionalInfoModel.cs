using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class AdditionalInfoModel
    {
        public string ContentKnowledge { get; set; }
        public string TotalCost { get; set; }
        public string StaffTotalCost { get; set; }
        public string PerCost { get; set; }
        public string AdditionalDetail { get; set; }
        public bool CafeRequire { get; set; }
        public string OfficerEmailPhone { get; set; }
    }
}