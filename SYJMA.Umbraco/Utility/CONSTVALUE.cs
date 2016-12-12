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
        public const string GET_EVENTNAMELIST = DOMAIN + "Tour/{0}/AllNames";
        public const string GET_EVENTFROMNAME = DOMAIN + "Tour/?tourname={0}&category={1}";
        public const string GET_ATTENDEETYPE = DOMAIN + "Tour/{0}/TourAttendeeType";
        public const string GET_YEARGROUP = DOMAIN + "LookupValue?lookupname=yeargroup&bomtype=event";//Update bomtype from event to tour
        public const string GET_SUBJECTAREA = DOMAIN + "LookupValue?lookupname=subjectarea&bomtype=event";//Update bomtype from event to tour
        public const string GET_SCHOOLCONTACTS = DOMAIN + "Contacts?primarycategory=Schools";

        public const string POST_TOURBOOKING = DOMAIN + "Tour/{0}/TourBooking";
        public const string POST_TOURBOOKINGATTENDEESUMMARY = DOMAIN + "Tour/{0}/TourBooking/{1}/TourBookingAttendeeSummary";
        
        public const string POST_CONTACT = DOMAIN + "Contacts/";
        public const string POST_TOURBOOKING_SUFFIX = "/TourBooking";

        public const string API_USERNAME = "SYJMA";
        public const string API_PASSWORD = "slowghost30";

        /// <summary>
        /// Content Names
        /// </summary>
        public const string SCHOOL_VISITS_CONTENT = "School Visits";
        public const string ADULT_VISITS_CONTENT = "Adult Visits";

        /// <summary>
        /// Constant partial view folder
        /// </summary>
        public const string PARTIAL_VIEW_SCHOOL_FOLDER = "~/Views/Partials/School/";
        public const string PARTIAL_VIEW_ADULT_FOLDER = "~/Views/Partials/Adult/";
        public const string PARTIAL_VIEW_UNIVERSITY_FOLDER = "~/Views/Partials/Uni/";

        public const int BOOK_COMPLETION_CONTENT_ID = 1210;
    }
}