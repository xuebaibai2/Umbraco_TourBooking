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

                school.SubTourIDList = Session["idList"] as List<int>;
                school.SubTourIDList.Add(int.Parse(id));
                Session["idList"] = school.SubTourIDList;

                school.Event.AdditionalInfo.OfficerEmailPhone = "1800 207 360";

                float studentPrice = school.AttendeeList
                .Where(x => x.Type.Equals(ATTENDEETYPE.ATTENDEETYPE_STUDENT))
                .Select(x => x.Cost).SingleOrDefault();

                school.Event.AdditionalInfo.PerCost = studentPrice.ToString("c2");
                school.Event.AdditionalInfo.TotalCost = GetTotalPrice(school.StudentsNumber, studentPrice).ToString("c2");
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
            
            school = contentController.GetSchoolModelById(school.Id);
            school.SubTourIDList = Session["idList"] as List<int>;
            string schoolSerialNumber = CreateNewSchoolContact(school);
            foreach (int id in school.SubTourIDList)
            {
                SchoolModel temp = contentController.GetSchoolModelById(id);
                temp.SerialNumber = schoolSerialNumber;
                SetSchoolSerialNumberInUmbraco(temp);
                CreateNewContact(temp);
                temp.TourBookingID = CreateNewTourBooking(temp);
                CreateNewTourBookingAttendeeSummary(temp);
                SetSchoolRecord(temp);
            }
            
            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());
            routeValues.Add("type", "School");
            if (school.Event.AdditionalInfo.CafeRequire)
            {
                //Do Something About Cafe Catering Sending Email?
            }
            return RedirectToUmbracoPage(1210, routeValues);
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

        private void SetSchoolRecord(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);

            schoolRecord.SetValue("tourBookingID", school.TourBookingID);
            schoolRecord.SetValue("contentKnowledge", school.Event.AdditionalInfo.ContentKnowledge);
            schoolRecord.SetValue("totalCost", school.Event.AdditionalInfo.TotalCost);
            schoolRecord.SetValue("perCost", school.Event.AdditionalInfo.PerCost);
            schoolRecord.SetValue("additionalDetails", school.Event.AdditionalInfo.AdditionalDetail);
            schoolRecord.SetValue("cafeRequirement", school.Event.AdditionalInfo.CafeRequire);

            Services.ContentService.Save(schoolRecord);
        }

        private string CreateNewTourBooking(SchoolModel school)
        {
            float totalCost = GetTotalPrice(school.StudentsNumber, school.GetStudentAttendeeCost())
                + GetTotalPrice(school.StaffNumber,school.GetStaffAttendeeCost());

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
            tourBooking.TOTALCOST = totalCost;
            tourBooking.YEARGROUP = school.Year;
            tourBooking.SUBJECT = school.SubjectArea;
            tourBooking.BOOKINGCOMMENT = school.Comments;

            return jsonDataController.PostNewTourBooking(school.Event.id, tourBooking).Trim('"');
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

                    attendeeSummary.ATTENDEETYPEID = school.GetStudentAttendeeID();
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

                    attendeeSummary.ATTENDEETYPEID = school.GetStaffAttendeeID();
                    attendeeSummary.QUANTITYBOOKED = school.StaffNumber;
                    attendeeSummary.QUANTITYATTENDED = school.StaffNumber;
                    attendeeSummary.ATTENDEECOST = GetTotalPrice(staffNumber, staffPrice);
                    attendeeSummary.DISCOUNT = discount;
                    attendeeSummary.FINALCOST = GetFinalPrice(attendeeSummary.ATTENDEECOST, attendeeSummary.DISCOUNT);
                    results.Add(jsonDataController.PostNewTourBookingAttendeeSummary(attendeeSummary));
                }
            }
            return results;
        }

        private string CreateNewSchoolContact(SchoolModel school)
        {
            school.SerialNumber = jsonDataController.PostNewContact<SchoolModel>(school, CONTACTTYPE.ORGANISATION).Trim('"');
            
            return school.SerialNumber;
        }

        private void SetSchoolSerialNumberInUmbraco(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("schoolSerialNumber", school.SerialNumber);
            Services.ContentService.Save(schoolRecord);
        }

        private void CreateNewContact(SchoolModel school)
        {
            if (school.Event.IsSameContact)
            {
                school.Event.GroupCoordinator.SerialNumber = jsonDataController.PostNewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.GROUPCOORDINATOR).Trim('"');
                school.Event.Invoice.SerialNumber = school.Event.GroupCoordinator.SerialNumber;
            }
            else
            {
                school.Event.GroupCoordinator.SerialNumber = jsonDataController.PostNewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.GROUPCOORDINATOR).Trim('"');
                school.Event.Invoice.SerialNumber = jsonDataController.PostNewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.INVOICEE).Trim('"');
            }
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("groupCoordinatorSerialNumber", school.Event.GroupCoordinator.SerialNumber);
            schoolRecord.SetValue("invoiceeSerialNumber", school.Event.Invoice.SerialNumber);
            Services.ContentService.Save(schoolRecord);
        }

        

        #endregion
    }
}