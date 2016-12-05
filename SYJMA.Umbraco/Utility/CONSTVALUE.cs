using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Utility
{
    /// <summary>
    /// Constant value used on this project
    /// </summary>
    public static class CONSTVALUE
    {

        //public const int SCHOOL_YEAR_DROPDOWNLIST_KEY = 1056;
        //public const int SCHOOL_SUBJECT_DROPDOWNLIST_KEY = 1057;

        /// <summary>
        /// The API Link
        /// </summary>
        //public const string TOUR_API = "http://demo2054938.mockable.io/";
        private const string DOMAIN = "http://localhost:58015/";
        //private const string DOMAIN = "https://www.thankq.net.au/apiREST/v105/";
        public const string TOUR_API = DOMAIN + "Tour/";

        public const string GET_EVENTFROMNAME = DOMAIN + "Tour?tourname=";
        public const string GET_ALLEVENTNAME = DOMAIN + "Tour/AllNames";
        public const string GET_ATTENDEETYPE = "/TourAttendeeType";
        public const string GET_YEARGROUP = DOMAIN + "LookupValue?lookupname=yeargroup&bomtype=event";
        public const string GET_SUBJECTAREA = DOMAIN + "LookupValue?lookupname=subjectarea&bomtype=event";
        public const string GET_SCHOOLCONTACTS = DOMAIN + "Contacts?primarycategory=Schools";

        public const string POST_CONTACT = DOMAIN + "Contacts/";
        public const string POST_TOURBOOKING_SUFFIX = "/TourBooking";
        public const string POST_TOURBOOKINGATTENDEESUMMARY_SUFFIX = "/TourBookingAttendeeSummary";
        public const string POST_TOURBOOKINGATTENDEESUMMARY_MIDDLE = "/TourBooking/";


        public const string API_USERNAME = "SYJMA";
        public const string API_PASSWORD = "slowghost30";
        /// <summary>
        /// Custom Datatype Names
        /// </summary>
        public const string SCHOOL_YEAR_DROPDOWNLIST_NAME = "SchoolYearDropdown";
        public const string SCHOOL_SUBJECT_DROPDOWNLIST_NAME = "SchoolSubjectDropdown";

        /// <summary>
        /// Content Names
        /// </summary>
        public const string SCHOOL_VISITS_CONTENT = "School Visits";

    }
}