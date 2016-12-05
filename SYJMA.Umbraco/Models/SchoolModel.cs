using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Utility;

namespace SYJMA.Umbraco.Models
{
    public class SchoolModel : BaseModel
    {
        public string SerialNumber { get; set; }
        public string SchoolName { get; set; }
        public string Year { get; set; }
        public string SubjectArea { get; set; }
        public int MainBookingID { get; set; }
        
        public IEnumerable<SelectListItem> YearList { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }
        public List<int> SubTourIDList = new List<int>();

        /// <summary>
        /// Temporary stored staff & student number, will retrive later from Attendee Model
        /// </summary>
        public int StaffNumber { get; set; }
        public int StudentsNumber { get; set; }

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