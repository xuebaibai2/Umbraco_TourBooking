using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class CalendarFormController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();
        private JSONDataController jsonDataController = new JSONDataController();

        /// <summary>
        /// Render Partial View based on the bookType and book model id
        /// </summary>
        /// <param name="bookType"></param>
        /// <param name="id"></param>
        /// <returns>Partial view based on the booktype and the model</returns>
        public PartialViewResult CalendarForm(string bookType, string id)
        {
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return PartialView("_Error");
            }

            if (bookType.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(id));
                if (school == null)
                {
                    return PartialView("_Error");
                }
                ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                ViewBag.parentUrl = ViewBag.rootUrl + "school-visits/";
                school.ProgramList = jsonDataController.GetEventNameList();
                return PartialView("~/Views/Partials/School/_SchoolCalendar.cshtml", school);
            }
            else if (bookType.Equals("Adult"))
            {

            }
            else if (bookType.Equals("University"))
            {

            }
            return null;
        }

        /// <summary>
        /// Retrieve data from privious page, insert data to umbraco and redirect to next page
        /// </summary>
        /// <param name="school"></param>
        /// <returns>Redirect to next page</returns>
        public ActionResult PostCalendarForm_School(SchoolModel school)
        {

            SetSchoolAttendeeDetail(school);
            contentController.SetPostCalendarForm_School(school);
            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("SchoolConfirm", CurrentPage), routeValues);
        }

        private void SetSchoolAttendeeDetail(SchoolModel school)
        {
            var attendeeList = jsonDataController.GetJsonData_AttendeeType(school.Event.id);
            if (attendeeList != null)
            {
                string studentAttendeeTypeID = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT)).Select(x => x.ID).FirstOrDefault();
                string staffAttendeeTypeID = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF)).Select(x => x.ID).FirstOrDefault();
                float studentPrice = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT)).Select(x => x.COST).FirstOrDefault();
                float staffPrice = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF)).Select(x => x.COST).FirstOrDefault();

                school.AttendeeList.Add(new Attendee
                {
                    ID = studentAttendeeTypeID,
                    Type = ATTENDEETYPE.ATTENDEETYPE_STUDENT,
                    Cost = studentPrice
                });
                school.AttendeeList.Add(new Attendee
                {
                    ID = staffAttendeeTypeID,
                    Type = ATTENDEETYPE.ATTENDEETYPE_STAFF,
                    Cost = staffPrice
                });
            }
            else
            {
                school.AttendeeList.Add(new Attendee
                {
                    ID = "",
                    Type = ATTENDEETYPE.ATTENDEETYPE_STUDENT,
                    Cost = 0
                });
                school.AttendeeList.Add(new Attendee
                {
                    ID = "",
                    Type = ATTENDEETYPE.ATTENDEETYPE_STAFF,
                    Cost = 0
                });
            }
        }

    }
}