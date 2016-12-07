using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYJMA.Umbraco.Models
{
    public class AdultModel : BaseModel
    {
        [Required(ErrorMessage = "Group Name is required")]
        public string GroupName { get; set; }
        
        [Required(ErrorMessage="Adult Number is required")]
        public int AdultNumber { get; set; }
        
    }
}