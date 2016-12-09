using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Models.API;
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public partial class JSONDataController : SurfaceController
    {
        public string CreateNewTourBookingOnThankQ(SchoolModel school)
        {
            float totalCost = GetTotalPrice(school.StudentsNumber, school.GetStudentAttendeeCost())
                + GetTotalPrice(school.StaffNumber, school.GetStaffAttendeeCost());

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
            return PostJsonData_NewTourBooking(school.Event.id, tourBooking).Trim('"');
        }

        public List<string> CreateNewTourBookingAttendeeSummaryOnThankQ(SchoolModel school)
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
                    results.Add(PostJsonData_NewTourBookingAttendeeSummary(attendeeSummary));
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
                    results.Add(PostJsonData_NewTourBookingAttendeeSummary(attendeeSummary));
                }
            }
            return results;
        }

        public string CreateNewSchoolContactOnThankQ(SchoolModel school)
        {
            school.SerialNumber = PostJsonData_NewContact<SchoolModel>(school, CONTACTTYPE.ORGANISATION).Trim('"');

            return school.SerialNumber;
        }

        //public void CreateNewContactOnThankQ(SchoolModel school)
        //{
        //    if (school.Event.IsSameContact)
        //    {
        //        school.Event.GroupCoordinator.SerialNumber = PostJsonData_NewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.GROUPCOORDINATOR).Trim('"');
        //        school.Event.Invoice.SerialNumber = school.Event.GroupCoordinator.SerialNumber;
        //    }
        //    else
        //    {
        //        school.Event.GroupCoordinator.SerialNumber = PostJsonData_NewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.GROUPCOORDINATOR).Trim('"');
        //        school.Event.Invoice.SerialNumber = PostJsonData_NewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.INVOICEE).Trim('"');
        //    }
        //}

        public void CreateNewContactOnThankQ<T>(BaseModel model)
        {
            if (typeof(T).Equals(new SchoolModel().GetType()))
            {
                SchoolModel school = (SchoolModel)model;
                if (school.Event.IsSameContact)
                {
                    school.Event.GroupCoordinator.SerialNumber = PostJsonData_NewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.GROUPCOORDINATOR).Trim('"');
                    school.Event.Invoice.SerialNumber = school.Event.GroupCoordinator.SerialNumber;
                }
                else
                {
                    school.Event.GroupCoordinator.SerialNumber = PostJsonData_NewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.GROUPCOORDINATOR).Trim('"');
                    school.Event.Invoice.SerialNumber = PostJsonData_NewContact<SchoolModel>(school, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.INVOICEE).Trim('"');
                }
            }
            else if (typeof(T).Equals(new AdultModel().GetType()))
            {
                AdultModel adult = (AdultModel)model;
                adult.Event.GroupCoordinator.SerialNumber = PostJsonData_NewContact<AdultModel>(adult, CONTACTTYPE.INDIVIDUAL, INDIVISUALTYPE.GROUPCOORDINATOR).Trim('"');
            }
            
        }

        private float GetTotalPrice(int studentNumber, float pricePerStudent)
        {
            return studentNumber * pricePerStudent;
        }

        private float GetFinalPrice(float o_price, float discount)
        {
            return o_price - discount;
        }
	}
}