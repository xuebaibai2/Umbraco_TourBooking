using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Utility;

namespace SYJMA.Umbraco.Models
{
    public class UniversityModel : BaseModel
    {
        public UniversityModel()
        {
            Payment = new Payment();
        }
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "University name is required to process further")]
        public string UniName { get; set; }

        public string CampusName { get; set; }

        [Required(ErrorMessage = "Staff Number is required")]
        public int StaffNumber { get; set; }

        [Required(ErrorMessage = "Student Number is required")]
        public int StudentNumber { get; set; }

        public Payment Payment { get; set; }

        public string GetStudentAttendeeID()
        {
            return this.AttendeeList.Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT))
                .Select(x => x.ID)
                .SingleOrDefault();
        }

        public string GetStaffAttendeeID()
        {
            return this.AttendeeList.Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF))
                .Select(x => x.ID)
                .SingleOrDefault();
        }

        public float GetStudentAttendeeCost()
        {
            return this.AttendeeList.Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT))
                .Select(x => x.Cost)
                .SingleOrDefault();
        }

        public float GetStaffAttendeeCost()
        {
            return this.AttendeeList.Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF))
                .Select(x => x.Cost)
                .SingleOrDefault();
        }
    }
}