using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYJMA.Umbraco.Models
{
    public class AdultModel : InitialModel
    {
        public string GroupName { get; set; }
        public string Program { get; set; }
        public int AdultNumber { get; set; }
        public IEnumerable<SelectListItem> ListOfProgram { get; set; }
    }
}