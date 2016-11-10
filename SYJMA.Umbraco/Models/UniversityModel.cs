using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYJMA.Umbraco.Models
{
    public class UniversityModel : InitialModel
    {
        public string UniName { get; set; }
        public string Program { get; set; }
        public int StaffNumber { get; set; }
        public int StudentNumber { get; set; }
        public IEnumerable<SelectListItem> ListOfProgram { get; set; }
    }
}