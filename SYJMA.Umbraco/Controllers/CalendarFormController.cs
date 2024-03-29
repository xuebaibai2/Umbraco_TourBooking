﻿using System;
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
using SYJMA.Umbraco.Models.ErrorModel;

namespace SYJMA.Umbraco.Controllers
{
    public class CalendarFormController : SurfaceController
    {
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
            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return contentController.GetPartialView_PageNotFound();
            }
            ViewBag.bookType = bookType;
            if (bookType.Equals(TOURCATEGORY.SCHOOL))
            {
                SchoolModel school = contentController.GetModelById_School(Convert.ToInt32(id));
                if (school == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                
                ViewBag.parentUrl = ViewBag.rootUrl + "school-visits/";
                school.ProgramList = jsonDataController.GetJsonData_EventNameList(TOURCATEGORY.SCHOOL);
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolCalendar.cshtml", school);
            }
            else if (bookType.Equals(TOURCATEGORY.ADULT))
            {
                AdultModel adult = contentController.GetModelById_Adult(Convert.ToInt32(id));
                if (adult == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }

                ViewBag.parentUrl = ViewBag.rootUrl + "adult-visits/";
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultCalendar.cshtml", adult);
            }
            else if (bookType.Equals(TOURCATEGORY.UNIVERSITY))
            {
                UniversityModel uni = contentController.GetModelById_University(Convert.ToInt32(id));
                if (uni == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }

                ViewBag.parentUrl = ViewBag.rootUrl + "university-visits/";
                return PartialView(CONSTVALUE.PARTIAL_VIEW_UNIVERSITY_FOLDER + "_UniCalendar.cshtml", uni);
            }
            return null;
        }

        /// <summary>
        /// Retrieve data from privious page, insert data to umbraco and redirect to next page
        /// </summary>
        /// <param name="school"></param>
        /// <returns>Redirect to next page</returns>
        [ValidateAntiForgeryToken]
        public ActionResult PostCalendarForm_School(SchoolModel school)
        {
            SetAttendeeDetail_School_Uni(school);
            school.Event.AdditionalInfo.StaffTotalCost = GetTotalPrice(school.StaffNumber, school.GetStaffAttendeeCost()).ToString("c2");
            school.Event.AdditionalInfo.TotalCost = GetTotalPrice(school.StudentsNumber, school.GetStudentAttendeeCost()).ToString("c2");
            contentController.SetPostCalendarForm_School(school);
            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());
            return RedirectToUmbracoPage(contentController.GetContentIDByName("SchoolConfirm"), routeValues);
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostCalendarForm_Adult(AdultModel adult)
        {
            SetAttendeeDetail_Adult(adult);
            adult.Event.AdditionalInfo.TotalCost = GetTotalPrice(adult.AdultNumber, adult.GetAdultAttendeeCost()).ToString("c2");
            contentController.SetPostCalendarForm_Adult(adult);
            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", adult.Id.ToString());
            return RedirectToUmbracoPage(contentController.GetContentIDByName("AdultConfirm"), routeValues);
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostCalendarForm_University(UniversityModel uni)
        {
            SetAttendeeDetail_School_Uni(uni);
            uni.Event.AdditionalInfo.StaffTotalCost= GetTotalPrice(uni.StaffNumber, uni.GetStaffAttendeeCost()).ToString("c2");
            uni.Event.AdditionalInfo.TotalCost = GetTotalPrice(uni.StudentNumber, uni.GetStudentAttendeeCost()).ToString("c2");
            contentController.SetPostCalendarForm_University(uni);
            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", uni.Id.ToString());
            return RedirectToUmbracoPage(contentController.GetContentIDByName("UniversityConfirm"), routeValues);
        }

        #region Private
        private void SetAttendeeDetail_Adult(AdultModel adult)
        {
            //Have to have one Attendee
            var attendeeList = jsonDataController.GetJsonData_AttendeeType(adult.Event.id);
            adult.AttendeeList.Add(new Attendee
            {
                ID = attendeeList.Where(x=>x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_ADULT)).Select(x=>x.ID).SingleOrDefault(),
                Type = ATTENDEETYPE.ATTENDEETYPE_ADULT,
                Cost = attendeeList.Where(x=>x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_ADULT)).Select(x=>x.COST).SingleOrDefault()
            });
        }

        private void SetAttendeeDetail_School_Uni(BaseModel model)
        {
            var attendeeList = jsonDataController.GetJsonData_AttendeeType(model.Event.id);
            if (attendeeList != null)
            {
                string studentAttendeeTypeID = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT))
                    .Select(x => x.ID).FirstOrDefault();
                string staffAttendeeTypeID = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF))
                    .Select(x => x.ID).FirstOrDefault();
                float studentPrice = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT))
                    .Select(x => x.COST).FirstOrDefault();
                float staffPrice = attendeeList.Where(x => x.TYPE.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF))
                    .Select(x => x.COST).FirstOrDefault();

                model.AttendeeList.Add(new Attendee
                {
                    ID = studentAttendeeTypeID,
                    Type = ATTENDEETYPE.ATTENDEETYPE_STUDENT,
                    Cost = studentPrice
                });
                model.AttendeeList.Add(new Attendee
                {
                    ID = staffAttendeeTypeID,
                    Type = ATTENDEETYPE.ATTENDEETYPE_STAFF,
                    Cost = staffPrice
                });
            }
            else
            {
                model.AttendeeList.Add(new Attendee
                {
                    ID = "",
                    Type = ATTENDEETYPE.ATTENDEETYPE_STUDENT,
                    Cost = 0
                });
                model.AttendeeList.Add(new Attendee
                {
                    ID = "",
                    Type = ATTENDEETYPE.ATTENDEETYPE_STAFF,
                    Cost = 0
                });
            }
        }

        private float GetTotalPrice(int number, float price)
        {
            return number * price;
        }
        #endregion
    }
}