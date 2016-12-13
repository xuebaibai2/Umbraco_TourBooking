using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Utility;

namespace SYJMA.Umbraco.Models
{
    public class AdultModel : BaseModel
    {
        [Required(ErrorMessage = "Group Name is required")]
        public string GroupName { get; set; }
        
        [Required(ErrorMessage="Adult Number is required")]
        public int AdultNumber { get; set; }

        public Payment Payment { get; set; }

        public float GetAdultAttendeeCost()
        {
            return this.AttendeeList.Select(x => x.Cost)
                .FirstOrDefault();
        }

        public string GetAdultAttendeeID()
        {
            return this.AttendeeList.Select(x => x.ID)
                .FirstOrDefault();
        }
    }
}