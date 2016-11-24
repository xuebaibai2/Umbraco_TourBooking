﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYJMA.Umbraco.Models
{
    public class SchoolModel : BaseModel
    {
        public string SerialNumber { get; set; }
        public string SchoolName { get; set; }
        public string Year { get; set; }
        public string SubjectArea { get; set; }
        public int StaffNumber { get; set; }
        public int StudentsNumber { get; set; }
        public IEnumerable<SelectListItem> YearList { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }
    }
}