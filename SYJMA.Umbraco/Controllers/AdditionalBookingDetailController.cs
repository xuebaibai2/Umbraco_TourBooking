using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Models.API;
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class AdditionalBookingDetailController : SurfaceController
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
        public PartialViewResult AdditionalBookingDetail(string bookType, string id)
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

                school.Event.AdditionalInfo.OfficerEmailPhone = "1800 207 360";

                float studentPrice = school.AttendeeList
                .Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT))
                .Select(x => x.Cost).SingleOrDefault();

                school.Event.AdditionalInfo.PerCost = "$" + studentPrice.ToString();
                school.Event.AdditionalInfo.TotalCost = "$" + GetTotalPrice(school.StudentsNumber, studentPrice).ToString();
                ViewBag.parentUrl = CurrentPage.Parent.Url + "?id=" + id;
                return PartialView("~/Views/Partials/School/_SchoolAdditionalBookingDetail.cshtml", school);
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
        public ActionResult PostAdditionalBooking_School(SchoolModel school)
        {
            SchoolModel tempSchool = contentController.GetSchoolModelById(school.Id);
            tempSchool.TourBookingID = CreateNewTourBooking(school.Event.id, tempSchool);
            var s = CreateNewTourBookingAttendeeSummary(tempSchool);
            var schoolRecord = Services.ContentService.GetById(school.Id);
            
            schoolRecord.SetValue("tourBookingID", tempSchool.TourBookingID);
            schoolRecord.SetValue("contentKnowledge", school.Event.AdditionalInfo.ContentKnowledge);
            schoolRecord.SetValue("totalCost", school.Event.AdditionalInfo.TotalCost);
            schoolRecord.SetValue("perCost", school.Event.AdditionalInfo.PerCost);
            schoolRecord.SetValue("additionalDetails", school.Event.AdditionalInfo.AdditionalDetail);
            schoolRecord.SetValue("cafeRequirement", school.Event.AdditionalInfo.CafeRequire);

            Services.ContentService.Save(schoolRecord);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());
            if (school.Event.AdditionalInfo.CafeRequire)
            {
                //Do Something About Cafe Catering Sending Email?
            }
            return CurrentUmbracoPage();
            //return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("", CurrentPage), routeValues);
        }

        #region 'Private Region'
        /// <summary>
        /// Get total student ticket price
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <param name="pricePerStudent"></param>
        /// <returns>Total price in float type</returns>
        private float GetTotalPrice(int studentNumber, float pricePerStudent)
        {
            return studentNumber * pricePerStudent;
        }

        private float GetFinalPrice(float o_price, float discount)
        {
            return o_price - discount;
        }

        private string CreateNewTourBooking(string tourID, SchoolModel school)
        {
            API_TOURBOOKING tourBooking = new API_TOURBOOKING();
            tourBooking.REFERENCE = "";
            tourBooking.TOURID = school.Event.id;
            tourBooking.STARTDATE = Convert.ToDateTime(school.Event.start).ToShortDateString();
            tourBooking.STARTTIME = Convert.ToDateTime(school.Event.start).ToString("HH:mm:ffff");
            tourBooking.ENDDATE = Convert.ToDateTime(school.Event.end).ToShortDateString();
            tourBooking.ENDTIME = Convert.ToDateTime(school.Event.end).ToString("HH:mm:ffff");
            tourBooking.STATUS = TOURBOOKINGSTATUS.BOOKED;
            tourBooking.BOOKERSERIALNUMBER = school.Event.GroupCoordinator.SerialNumber;
            tourBooking.FORSERIALNUMBER = school.SerialNumber;
            tourBooking.INVOICEESERIALNUMBER = school.Event.Invoice.SerialNumber;
            tourBooking.YEARGROUP = school.Year;
            tourBooking.SUBJECT = school.Event.title;
            tourBooking.BOOKINGCOMMENT = school.Comments;

            return jsonDataController.PostNewTourBooking(tourID, tourBooking).Trim('"');
        }

        private List<string> CreateNewTourBookingAttendeeSummary(SchoolModel school)
        {
            List<string> results = new List<string>();
            API_TOURBOOKINGATTENDEESUMMARY attendeeSummary = new API_TOURBOOKINGATTENDEESUMMARY();
            attendeeSummary.TOURID = school.Event.id;
            attendeeSummary.TOURBOOKINGID = school.TourBookingID;

            for (int i = 0; i < school.AttendeeList.Count; i++)
            {
                if (school.AttendeeList[i].Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT))
                {
                    float studentPrice = school.GetStudentAttendeeCost();
                    int studentNumber = school.StudentsNumber;
                    float discount = 0;

                    attendeeSummary.ATTENDEETYPEID = school.AttendeeList.Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT)).Select(x => x.ID).SingleOrDefault();
                    attendeeSummary.QUANTITYBOOKED = school.StudentsNumber;
                    attendeeSummary.QUANTITYATTENDED = school.StudentsNumber;
                    attendeeSummary.ATTENDEECOST = GetTotalPrice(studentNumber, studentPrice);
                    attendeeSummary.DISCOUNT = discount;
                    attendeeSummary.FINALCOST = GetFinalPrice(attendeeSummary.ATTENDEECOST, attendeeSummary.DISCOUNT);
                    results.Add(jsonDataController.PostNewTourBookingAttendeeSummary(attendeeSummary));
                }
                else if (school.AttendeeList[i].Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF))
                {
                    float staffPrice = school.GetStaffAttendeeCost();
                    int staffNumber = school.StaffNumber;
                    float discount = 0;

                    attendeeSummary.ATTENDEETYPEID = school.AttendeeList.Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STAFF)).Select(x => x.ID).SingleOrDefault();
                    attendeeSummary.QUANTITYBOOKED = school.StudentsNumber;
                    attendeeSummary.QUANTITYATTENDED = school.StudentsNumber;
                    attendeeSummary.ATTENDEECOST = GetTotalPrice(staffNumber, staffPrice);
                    attendeeSummary.DISCOUNT = discount;
                    attendeeSummary.FINALCOST = GetFinalPrice(attendeeSummary.ATTENDEECOST, attendeeSummary.DISCOUNT);
                    results.Add(jsonDataController.PostNewTourBookingAttendeeSummary(attendeeSummary));
                }
            }
            return results;
        }

        #endregion
    }
}