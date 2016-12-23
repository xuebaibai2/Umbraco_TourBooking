using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SYJMA.Umbraco.Utility
{
    /// <summary>
    /// Constant value used on this project
    /// </summary>
    public struct CONSTVALUE
    {
        /// <summary>
        /// The API Link
        /// </summary>
        //public const string TOUR_API = "http://demo2054938.mockable.io/";
        //private const string DOMAIN = "http://localhost:58015/";
        //private const string DOMAIN = "https://www.thankq.net.au/apiREST/v105/";
        private static readonly string DOMAIN = ConfigurationManager.AppSettings["API_Domain"];
        public static readonly string API_USERNAME = ConfigurationManager.AppSettings["API_USERNAME"];
        public static readonly string API_PASSWORD = ConfigurationManager.AppSettings["API_PASSWORD"];
        public static readonly string TOUR_API = DOMAIN + "Tour/";
        public static readonly string GET_EVENTNAMELIST = DOMAIN + "Tour/{0}/AllNames";
        public static readonly string GET_EVENTFROMNAME = DOMAIN + "Tour/?tourname={0}&category={1}";
        public static readonly string GET_ATTENDEETYPE = DOMAIN + "Tour/{0}/TourAttendeeType";
        public static readonly string GET_YEARGROUP = DOMAIN + "LookupValue?lookupname=yeargroup&bomtype=event";//Update bomtype from event to tour
        public static readonly string GET_SUBJECTAREA = DOMAIN + "LookupValue?lookupname=subjectarea&bomtype=event";//Update bomtype from event to tour
        public static readonly string GET_SCHOOLCONTACTS = DOMAIN + "Contacts?primarycategory=Schools";
        public static readonly string GET_UNICONTACTS = DOMAIN + "Contacts?primarycategory=University";
        public static readonly string POST_TOURBOOKING = DOMAIN + "Tour/{0}/TourBooking";
        public static readonly string POST_TOURBOOKINGATTENDEESUMMARY = DOMAIN + "Tour/{0}/TourBooking/{1}/TourBookingAttendeeSummary";
        public static readonly string POST_CONTACT = DOMAIN + "Contacts/";
        public static readonly string POST_TOURBOOKING_SUFFIX = "/TourBooking";

        /// <summary>
        /// Content Names
        /// </summary>
        public const string SCHOOL_VISITS_CONTENT = "School Visits";
        public const string ADULT_VISITS_CONTENT = "Adult Visits";
        public const string UNIVERSITY_VISITS_CONTENT = "University Visits";

        /// <summary>
        /// Constant partial view folder
        /// </summary>
        public const string PARTIAL_VIEW_SCHOOL_FOLDER = "~/Views/Partials/School/";
        public const string PARTIAL_VIEW_ADULT_FOLDER = "~/Views/Partials/Adult/";
        public const string PARTIAL_VIEW_UNIVERSITY_FOLDER = "~/Views/Partials/Uni/";
        public const string PARTIAL_VIEW_ERROR_FOLDER = "~/Views/Partials/Error/";

        public const int BOOK_COMPLETION_CONTENT_ID = 1210;
        public const int PAGE_NOT_FOUND_CONTENT_ID = 1451;

        public const string PAGE_NOT_FOUND_CONTENT_NAME = "Page Not Found";
    }
}