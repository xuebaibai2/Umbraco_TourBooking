using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using SYJMA.Umbraco.Models.ErrorModel;
using umbraco;

namespace SYJMA.Umbraco.Controllers
{
    public class ContentController : SurfaceController
    {

        /// <summary>
        /// Get School object from Umbraco record  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>School Model</returns>
        internal SchoolModel GetModelById_School(int id)
        {
            var data = ApplicationContext.Services.ContentService.GetById(id);
            if (data == null || data.Parent() == null)
            {
                return null;
            }
            else if (!data.Parent().Name.Equals(CONSTVALUE.SCHOOL_VISITS_CONTENT))
            {
                try
                {
                    if (data.GetValue("mainBookingID").Equals(data.Parent().GetValue("recordId")))
                    {
                        return GetModel_School(data);
                    }
                }
                catch (KeyNotFoundException)
                {

                    return null;
                }
                
                return null;
            }
            else
            {
                return GetModel_School(data);
            }
        }

        internal AdultModel GetModelById_Adult(int id)
        {
            var data = ApplicationContext.Services.ContentService.GetById(id);
            if (data == null || data.Parent() == null)
            {
                return null;
            }
            else if (!data.Parent().Name.Equals(CONSTVALUE.ADULT_VISITS_CONTENT))
            {
                return null;
            }
            else
            {
                return GetModel_Adult(data);
            }
        }

        internal UniversityModel GetModelById_University(int id)
        {
            var data = ApplicationContext.Services.ContentService.GetById(id);
            if (data == null || data.Parent() == null)
            {
                return null;
            }
            else if (!data.Parent().Name.Equals(CONSTVALUE.UNIVERSITY_VISITS_CONTENT))
            {
                return null;
            }
            else
            {
                return GetModel_University(data);
            }
        }

        internal PartialViewResult GetPartialView_PageNotFound()
        {
            var contentID = uQuery.GetNodesByName(CONSTVALUE.PAGE_NOT_FOUND_CONTENT_NAME).FirstOrDefault().Id; ;
            IContent data = ApplicationContext.Services.ContentService.GetById(contentID);
            PageNotFound pageNotFoundModel =  new PageNotFound()
            {
                ErrorCode = Convert.ToString(data.GetValue("errorCode")),
                ErrorTitle = Convert.ToString(data.GetValue("errorTitle")),
                ErrorDescription = Convert.ToString(data.GetValue("errorDescription")),
            };
            return PartialView(CONSTVALUE.PARTIAL_VIEW_ERROR_FOLDER + "_404.cshtml", pageNotFoundModel);
        }

        public void CreateNewSchoolModel(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.CreateContent("Creating another booking for " + school.SchoolName, school.MainBookingID, "School");

            Services.ContentService.SaveAndPublishWithStatus(schoolRecord);
            school.Id = schoolRecord.Id;

            schoolRecord.SetValue("recordId", school.Id);
            schoolRecord.SetValue("mainBookingID", school.MainBookingID);
            schoolRecord.SetValue("schoolSerialNumber", school.SerialNumber);
            schoolRecord.SetValue("nameOfSchool", school.SchoolName);
            schoolRecord.SetValue("year", school.Year);
            schoolRecord.SetValue("preferredDateSchool", school.PreferredDate);
            schoolRecord.SetValue("subjectArea", school.SubjectArea);
            schoolRecord.SetValue("numberOfStudents", school.StudentsNumber);
            schoolRecord.SetValue("numberOfStaff", school.StaffNumber);

            schoolRecord.SetValue("groupCoordinatorSerialNumber", school.Event.GroupCoordinator.SerialNumber);
            schoolRecord.SetValue("title", school.Event.GroupCoordinator.Title);
            schoolRecord.SetValue("firstName", school.Event.GroupCoordinator.FirstName);
            schoolRecord.SetValue("surename", school.Event.GroupCoordinator.SureName);
            schoolRecord.SetValue("email", school.Event.GroupCoordinator.Email);
            schoolRecord.SetValue("mobile", school.Event.GroupCoordinator.Mobile);
            schoolRecord.SetValue("daytimeNumber", school.Event.GroupCoordinator.DaytimeNumber);

            schoolRecord.SetValue("invoiceeSerialNumber", school.Event.Invoice.SerialNumber);
            schoolRecord.SetValue("invoiceTitle", school.Event.Invoice.Title);
            schoolRecord.SetValue("invoiceFirstName", school.Event.Invoice.FirstName);
            schoolRecord.SetValue("invoiceSurename", school.Event.Invoice.SureName);
            schoolRecord.SetValue("invoiceEmail", school.Event.Invoice.Email);
            Services.ContentService.Save(schoolRecord);
        }

        public void SetPostSubTourInitialPage_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("year", school.Year);
            schoolRecord.SetValue("preferredDateSchool", GetDateTimeForPost(school));
            schoolRecord.SetValue("subjectArea", school.SubjectArea);
            schoolRecord.SetValue("numberOfStudents", school.StudentsNumber);
            schoolRecord.SetValue("numberOfStaff", school.StaffNumber);
            schoolRecord.SetValue("comments", school.Comments);
            schoolRecord.Name = string.Format("{0} - Year Group: {1}", school.Id, school.Year);
            Services.ContentService.Save(schoolRecord);
        }

        public void SetPostInitialPage_School(SchoolModel school, IPublishedContent currentPage)
        {
            var schoolRecord = Services.ContentService.CreateContent(school.SchoolName + " - " + school.SubjectArea, currentPage.Id, "School");
            schoolRecord.SetValue("nameOfSchool", school.SchoolName);
            schoolRecord.SetValue("year", school.Year);
            schoolRecord.SetValue("preferredDateSchool", GetDateTimeForPost(school));
            schoolRecord.SetValue("subjectArea", school.SubjectArea);
            schoolRecord.SetValue("numberOfStudents", school.StudentsNumber);
            schoolRecord.SetValue("numberOfStaff", school.StaffNumber);
            schoolRecord.SetValue("comments", school.Comments);
            Services.ContentService.SaveAndPublishWithStatus(schoolRecord);
            school.Id = schoolRecord.Id;
            schoolRecord.SetValue("recordId", school.Id);
            school.MainBookingID = school.Id;
            schoolRecord.SetValue("mainBookingID", school.MainBookingID);
            schoolRecord.Name = string.Format("{0} - {1}", school.MainBookingID, school.SchoolName);
            Services.ContentService.Save(schoolRecord);
        }

        internal void SetPostInitialPage_University(UniversityModel uni, IPublishedContent CurrentPage)
        {
            var uniRecord = Services.ContentService.CreateContent(uni.UniName + " - " + uni.Program, CurrentPage.Id, "University");

            uniRecord.SetValue("nameOfUniversity", uni.UniName);
            uniRecord.SetValue("nameOfCampus", uni.CampusName);
            uniRecord.SetValue("program", uni.Program);
            uniRecord.SetValue("preferredDate", GetDateTimeForPost(uni));
            uniRecord.SetValue("numberOfStudents", uni.StudentNumber);
            uniRecord.SetValue("numberOfStaff", uni.StaffNumber);
            uniRecord.SetValue("comments", uni.Comments);
            Services.ContentService.SaveAndPublishWithStatus(uniRecord);
            uni.Id = uniRecord.Id;
            uniRecord.SetValue("recordId", uni.Id);
            uniRecord.Name = string.Format("{0} - {1} - {2}", uni.Id, uni.UniName, uni.CampusName);
            Services.ContentService.Save(uniRecord);
        }

        internal void SetPostInitialPage_Adult(AdultModel adult, IPublishedContent CurrentPage)
        {
            var adultRecord = Services.ContentService.CreateContent(adult.GroupName + " - " + adult.Program, CurrentPage.Id, "Adult");

            adultRecord.SetValue("nameOfGroup", adult.GroupName);
            adultRecord.SetValue("program", adult.Program);
            adultRecord.SetValue("preferredDateAdult", GetDateTimeForPost(adult));
            adultRecord.SetValue("numberOfAdults", adult.AdultNumber);
            adultRecord.SetValue("comments", adult.Comments);
            Services.ContentService.SaveAndPublishWithStatus(adultRecord);
            adult.Id = adultRecord.Id;
            adultRecord.SetValue("recordId", adult.Id);
            adultRecord.Name = string.Format("{0} - {1}", adult.Id, adult.GroupName);
            Services.ContentService.Save(adultRecord);
        }

        public void SetPostCalendarForm_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("studentAttendeeTypeID", school.GetStudentAttendeeID());
            schoolRecord.SetValue("staffAttendeeTypeID", school.GetStaffAttendeeID() ?? "N/A");
            schoolRecord.SetValue("eventTitle", school.Event.title);
            schoolRecord.SetValue("eventId", school.Event.id);
            schoolRecord.SetValue("eventStart", school.Event.start);
            schoolRecord.SetValue("eventEnd", school.Event.end);
            schoolRecord.SetValue("eventPriceStudent", school.GetStudentAttendeeCost().ToString("c2"));
            schoolRecord.SetValue("eventPriceStaff", school.GetStaffAttendeeCost().ToString("c2"));
            schoolRecord.SetValue("totalCost", school.Event.AdditionalInfo.TotalCost);
            schoolRecord.SetValue("staffTotalCost", school.Event.AdditionalInfo.StaffTotalCost);
            Services.ContentService.Save(schoolRecord);
        }

        internal void SetPostCalendarForm_Adult(AdultModel adult)
        {
            var adultRecord = Services.ContentService.GetById(adult.Id);
            adultRecord.SetValue("attendeeTypeID", adult.AttendeeList.First().ID);
            adultRecord.SetValue("eventTitle", adult.Event.title);
            adultRecord.SetValue("eventId", adult.Event.id);
            adultRecord.SetValue("eventStart", adult.Event.start);
            adultRecord.SetValue("eventEnd", adult.Event.end);
            adultRecord.SetValue("eventPrice", adult.AttendeeList.Single().Cost.ToString("c2"));
            adultRecord.SetValue("totalCost", adult.Event.AdditionalInfo.TotalCost);
            adultRecord.SetValue("isInvoiceOnly", adult.Event.IsInvoiceOnly);
            Services.ContentService.Save(adultRecord);
        }

        internal void SetPostCalendarForm_University(UniversityModel uni)
        {
            var uniRecord = Services.ContentService.GetById(uni.Id);
            uniRecord.SetValue("studentAttendeeTypeID", uni.GetStudentAttendeeID());
            uniRecord.SetValue("staffAttendeeTypeID", uni.GetStaffAttendeeID() ?? "N/A");
            uniRecord.SetValue("eventTitle", uni.Event.title);
            uniRecord.SetValue("eventId", uni.Event.id);
            uniRecord.SetValue("eventStart", uni.Event.start);
            uniRecord.SetValue("eventEnd", uni.Event.end);
            uniRecord.SetValue("eventPriceStudent", uni.GetStudentAttendeeCost().ToString("c2"));
            uniRecord.SetValue("eventPriceStaff", uni.GetStaffAttendeeCost().ToString("c2"));
            uniRecord.SetValue("totalCost", uni.Event.AdditionalInfo.TotalCost);
            uniRecord.SetValue("staffTotalCost", uni.Event.AdditionalInfo.StaffTotalCost);
            uniRecord.SetValue("isInvoiceOnly", uni.Event.IsInvoiceOnly);
            Services.ContentService.Save(uniRecord);
        }

        public void SetPostBooking_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("title", school.Event.GroupCoordinator.Title);
            schoolRecord.SetValue("firstName", school.Event.GroupCoordinator.FirstName);
            schoolRecord.SetValue("surename", school.Event.GroupCoordinator.SureName);
            schoolRecord.SetValue("email", school.Event.GroupCoordinator.Email);
            schoolRecord.SetValue("mobile", school.Event.GroupCoordinator.Mobile);
            schoolRecord.SetValue("daytimeNumber", school.Event.GroupCoordinator.DaytimeNumber);
            schoolRecord.SetValue("invoiceTitle", school.Event.Invoice.Title);
            schoolRecord.SetValue("invoiceFirstName", school.Event.Invoice.FirstName);
            schoolRecord.SetValue("invoiceSurename", school.Event.Invoice.SureName);
            schoolRecord.SetValue("invoiceEmail", school.Event.Invoice.Email);
            schoolRecord.SetValue("isSameContact", school.Event.IsSameContact);
            Services.ContentService.Save(schoolRecord);
        }

        internal void SetPostBooking_Adult(AdultModel adult)
        {
            var adultRecord = Services.ContentService.GetById(adult.Id);
            adultRecord.SetValue("title", adult.Event.GroupCoordinator.Title);
            adultRecord.SetValue("firstName", adult.Event.GroupCoordinator.FirstName);
            adultRecord.SetValue("surename", adult.Event.GroupCoordinator.SureName);
            adultRecord.SetValue("email", adult.Event.GroupCoordinator.Email);
            adultRecord.SetValue("mobile", adult.Event.GroupCoordinator.Mobile);
            adultRecord.SetValue("daytimeNumber", adult.Event.GroupCoordinator.DaytimeNumber);
            adultRecord.SetValue("mobile", adult.Event.GroupCoordinator.Mobile);
            adultRecord.SetValue("address", adult.Event.GroupCoordinator.Address);
            adultRecord.SetValue("suburb", adult.Event.GroupCoordinator.Suburb);
            adultRecord.SetValue("state", adult.Event.GroupCoordinator.State);
            adultRecord.SetValue("postcode", adult.Event.GroupCoordinator.Postcode);
            Services.ContentService.Save(adultRecord);
        }

        internal void SetPostBooking_University(UniversityModel uni)
        {
            var uniRecord = Services.ContentService.GetById(uni.Id);
            uniRecord.SetValue("title", uni.Event.GroupCoordinator.Title);
            uniRecord.SetValue("firstName", uni.Event.GroupCoordinator.FirstName);
            uniRecord.SetValue("surename", uni.Event.GroupCoordinator.SureName);
            uniRecord.SetValue("email", uni.Event.GroupCoordinator.Email);
            uniRecord.SetValue("mobile", uni.Event.GroupCoordinator.Mobile);
            uniRecord.SetValue("daytimeNumber", uni.Event.GroupCoordinator.DaytimeNumber);
            uniRecord.SetValue("mobile", uni.Event.GroupCoordinator.Mobile);
            uniRecord.SetValue("address", uni.Event.GroupCoordinator.Address);
            uniRecord.SetValue("suburb", uni.Event.GroupCoordinator.Suburb);
            uniRecord.SetValue("state", uni.Event.GroupCoordinator.State);
            uniRecord.SetValue("postcode", uni.Event.GroupCoordinator.Postcode);
            Services.ContentService.Save(uniRecord);
        }

        internal void SetPostInvoice_Adult(AdultModel adult)
        {
            var schoolRecord = Services.ContentService.GetById(adult.Id);
            schoolRecord.SetValue("invoiceTitle", adult.Event.Invoice.Title);
            schoolRecord.SetValue("invoiceFirstName", adult.Event.Invoice.FirstName);
            schoolRecord.SetValue("invoiceSurename", adult.Event.Invoice.SureName);
            schoolRecord.SetValue("invoiceEmail", adult.Event.Invoice.Email);
            schoolRecord.SetValue("invoiceJobTitle", adult.Event.Invoice.JobTitle);
            schoolRecord.SetValue("invoiceCompany", adult.Event.Invoice.Company);
            schoolRecord.SetValue("invoicePhone", adult.Event.Invoice.Phone);
            Services.ContentService.Save(schoolRecord);
        }

        internal void SetPostInvoice_University(UniversityModel uni)
        {
            var adultRecord = Services.ContentService.GetById(uni.Id);
            adultRecord.SetValue("invoiceTitle", uni.Event.Invoice.Title);
            adultRecord.SetValue("invoiceFirstName", uni.Event.Invoice.FirstName);
            adultRecord.SetValue("invoiceSurename", uni.Event.Invoice.SureName);
            adultRecord.SetValue("invoiceEmail", uni.Event.Invoice.Email);
            adultRecord.SetValue("invoiceJobTitle", uni.Event.Invoice.JobTitle);
            adultRecord.SetValue("invoiceInstitution", uni.Event.Invoice.Institution);
            adultRecord.SetValue("invoicePhone", uni.Event.Invoice.Phone);
            Services.ContentService.Save(adultRecord);
        }

        public void SetPostAdditionalBooking_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("contentKnowledge", school.Event.AdditionalInfo.ContentKnowledge);
            schoolRecord.SetValue("additionalDetails", school.Event.AdditionalInfo.AdditionalDetail);
            schoolRecord.SetValue("cafeRequirement", school.Event.AdditionalInfo.CafeRequire);
            schoolRecord.SetValue("schoolSerialNumber", school.SerialNumber);
            schoolRecord.SetValue("tourBookingID", school.TourBookingID);
            schoolRecord.SetValue("groupCoordinatorSerialNumber", school.Event.GroupCoordinator.SerialNumber);
            schoolRecord.SetValue("invoiceeSerialNumber", school.Event.Invoice.SerialNumber);
            Services.ContentService.Save(schoolRecord);
        }

        public void SetPostAdditionalBooking_Adult(AdultModel adult)
        {
            var adultRecord = Services.ContentService.GetById(adult.Id);
            adultRecord.SetValue("tourBookingID", adult.TourBookingID);
            adultRecord.SetValue("groupCoordinatorSerialNumber", adult.Event.GroupCoordinator.SerialNumber);
            adultRecord.SetValue("invoiceeSerialNumber", adult.Event.Invoice.SerialNumber);
            adultRecord.SetValue("paymentFingerprint", adult.Payment.EPS_FINGERPRINT);
            Services.ContentService.Save(adultRecord);
        }

        internal void SetPostAdditionalBooking_University(UniversityModel uni)
        {
            var uniRecord = Services.ContentService.GetById(uni.Id);
            uniRecord.SetValue("tourBookingID", uni.TourBookingID);
            uniRecord.SetValue("groupCoordinatorSerialNumber", uni.Event.GroupCoordinator.SerialNumber);
            uniRecord.SetValue("invoiceeSerialNumber", uni.Event.Invoice.SerialNumber);
            uniRecord.SetValue("paymentFingerprint", uni.Payment.EPS_FINGERPRINT);
            uniRecord.SetValue("uniSerialNumber", uni.SerialNumber);
            Services.ContentService.Save(uniRecord);
        }

        internal void SetPaymentFingerprint_Adult(int id, string fingerPrint)
        {
            var adultRecord = Services.ContentService.GetById(id);
            adultRecord.SetValue("paymentFingerprint", fingerPrint);
            Services.ContentService.Save(adultRecord);
        }

        /// <summary>
        /// Get Content ID based on content name
        /// </summary>
        /// <param name="contentName"></param>
        /// <returns>Content ID</returns>
        internal int GetContentIDByName(string contentName)
        {
            return uQuery.GetNodesByName(contentName).FirstOrDefault().Id;
        }

        #region 'Private Region'

        private SchoolModel GetModel_School(IContent data)
        {
            EventCalendar eventCalendar = GetEventCalendar_School(data);
            List<Attendee> attendeeList = GetAttendeeList_School(data);
            SchoolModel model = new SchoolModel()
            {
                Id = Convert.ToInt32(data.GetValue("recordId")),
                MainBookingID = Convert.ToInt32(data.GetValue("mainBookingID")),
                SerialNumber = Convert.ToString(data.GetValue("schoolSerialnumber")),
                Year = Convert.ToString(data.GetValue("year")),
                SchoolName = Convert.ToString(data.GetValue("nameOfSchool")),
                PreferredDate = Convert.ToString(data.GetValue("preferredDateSchool")),
                SubjectArea = Convert.ToString(data.GetValue("subjectArea")),
                StudentsNumber = Convert.ToInt32(data.GetValue("numberOfStudents")),
                StaffNumber = Convert.ToInt32(data.GetValue("numberOfStaff")),
                Comments = Convert.ToString(data.GetValue("comments")),
                TourBookingID = Convert.ToString(data.GetValue("tourBookingID")),
                Event = eventCalendar,
                AttendeeList = attendeeList
            };
            return model;
        }

        private AdultModel GetModel_Adult(IContent data)
        {
            EventCalendar eventCalendar = GetEventCalendar_Adult(data);
            List<Attendee> attendeeList = GetAttendeeList_Adult(data);
            AdultModel model = new AdultModel()
            {
                Id = Convert.ToInt32(data.GetValue("recordId")),
                PreferredDate = Convert.ToString(data.GetValue("preferredDateAdult")),
                AdultNumber = Convert.ToInt32(data.GetValue("numberOfAdults")),
                Comments = Convert.ToString(data.GetValue("comments")),
                TourBookingID = Convert.ToString(data.GetValue("tourBookingID")),
                Program = Convert.ToString(data.GetValue("program")),
                GroupName = Convert.ToString(data.GetValue("nameofGroup")),
                Event = eventCalendar,
                AttendeeList = attendeeList
            };
            return model;
        }


        private UniversityModel GetModel_University(IContent data)
        {
            EventCalendar eventCalendar = GetEventCalendar_University(data);
            List<Attendee> attendeeList = GetAttendeeList_University(data);
            UniversityModel model = new UniversityModel()
            {
                Id = Convert.ToInt32(data.GetValue("recordId")),
                UniName= Convert.ToString(data.GetValue("nameofUniversity")),
                CampusName = Convert.ToString(data.GetValue("nameofCampus")),
                PreferredDate = Convert.ToString(data.GetValue("preferredDate")),
                StudentNumber = Convert.ToInt32(data.GetValue("numberofStudents")),
                StaffNumber = Convert.ToInt32(data.GetValue("numberofStaff")),
                Comments = Convert.ToString(data.GetValue("comments")),
                TourBookingID = Convert.ToString(data.GetValue("tourBookingID")),
                Program = Convert.ToString(data.GetValue("program")),
                Event = eventCalendar,
                AttendeeList = attendeeList
            };
            return model;
        }


        private DateTime GetDateTimeForPost(BaseModel viewModel)
        {
            return Convert.ToDateTime(viewModel.PreferredDate, new System.Globalization.CultureInfo("en-AU", true));
        } 

        private List<Attendee> GetAttendeeList_School(IContent data)
        {
            string _studentPrice = Convert.ToString(data.GetValue("eventPriceStudent"));
            float studentPrice = _studentPrice.Equals("") ? 0 : float.Parse(_studentPrice, NumberStyles.Currency);
            string _staffPrice = Convert.ToString(data.GetValue("eventPriceStaff"));
            float staffPrice = _staffPrice.Equals("") ? 0 : float.Parse(_staffPrice, NumberStyles.Currency);
            List<Attendee> temp = new List<Attendee>();
            temp.Add(new Attendee()
            {
                ID = Convert.ToString(data.GetValue("studentAttendeeTypeID") ?? ""),
                Cost = studentPrice,
                Type = ATTENDEETYPE.ATTENDEETYPE_STUDENT
            });
            temp.Add(new Attendee()
            {
                ID = Convert.ToString(data.GetValue("staffAttendeeTypeID") ?? "N/A"),
                Cost = staffPrice,
                Type = ATTENDEETYPE.ATTENDEETYPE_STAFF
            });
            return temp;
        }

        private List<Attendee> GetAttendeeList_Adult(IContent data)
        {
            string _adultPrice = Convert.ToString(data.GetValue("eventPrice"));
            float adultPrice = _adultPrice.Equals("") ? 0 : float.Parse(_adultPrice, NumberStyles.Currency);
            List<Attendee> temp = new List<Attendee>();
            temp.Add(new Attendee()
            {
                ID = Convert.ToString(data.GetValue("attendeeTypeID") ?? ""),
                Cost = adultPrice,
                Type = ATTENDEETYPE.ATTENDEETYPE_ADULT
            });
            return temp;
        }

        private List<Attendee> GetAttendeeList_University(IContent data)
        {
            string _studentPrice = Convert.ToString(data.GetValue("eventPriceStudent"));
            float studentPrice = _studentPrice.Equals("") ? 0 : float.Parse(_studentPrice, NumberStyles.Currency);
            string _staffPrice = Convert.ToString(data.GetValue("eventPriceStaff"));
            float staffPrice = _staffPrice.Equals("") ? 0 : float.Parse(_staffPrice, NumberStyles.Currency);
            List<Attendee> temp = new List<Attendee>();
            temp.Add(new Attendee()
            {
                ID = Convert.ToString(data.GetValue("studentAttendeeTypeID") ?? ""),
                Cost = studentPrice,
                Type = ATTENDEETYPE.ATTENDEETYPE_STUDENT
            });
            temp.Add(new Attendee()
            {
                ID = Convert.ToString(data.GetValue("staffAttendeeTypeID") ?? "N/A"),
                Cost = staffPrice,
                Type = ATTENDEETYPE.ATTENDEETYPE_STAFF
            });
            return temp;
        }


        /// <summary>
        /// Get Invoice object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Invoice Model</returns>
        private Invoice GetInvoice_School(IContent data)
        {
            return new Invoice()
            {
                Title = Convert.ToString(data.GetValue("invoiceTitle") ?? ""),
                FirstName = Convert.ToString(data.GetValue("invoiceFirstName") ?? ""),
                SureName = Convert.ToString(data.GetValue("invoiceSurename") ?? ""),
                Email = Convert.ToString(data.GetValue("invoiceEmail") ?? ""),
                SerialNumber = Convert.ToString(data.GetValue("invoiceeSerialNumber") ?? "")
            };
        }

        private Invoice GetInvoice_Adult(IContent data)
        {
            return new Invoice()
            {
                Title = Convert.ToString(data.GetValue("invoiceTitle") ?? ""),
                FirstName = Convert.ToString(data.GetValue("invoiceFirstName") ?? ""),
                SureName = Convert.ToString(data.GetValue("invoiceSurename") ?? ""),
                Email = Convert.ToString(data.GetValue("invoiceEmail") ?? ""),
                SerialNumber = Convert.ToString(data.GetValue("invoiceeSerialNumber") ?? ""),
                Company = Convert.ToString(data.GetValue("invoiceCompany") ?? ""),
                JobTitle = Convert.ToString(data.GetValue("invoiceJobTitle") ?? ""),
                Phone = Convert.ToString(data.GetValue("invoicePhone") ?? ""),
            };
        }

        private Invoice GetInvoice_University(IContent data)
        {
            return new Invoice()
            {
                Title = Convert.ToString(data.GetValue("invoiceTitle") ?? ""),
                FirstName = Convert.ToString(data.GetValue("invoiceFirstName") ?? ""),
                SureName = Convert.ToString(data.GetValue("invoiceSurename") ?? ""),
                Email = Convert.ToString(data.GetValue("invoiceEmail") ?? ""),
                SerialNumber = Convert.ToString(data.GetValue("invoiceeSerialNumber") ?? ""),
                Institution = Convert.ToString(data.GetValue("invoiceInstitution") ?? ""),
                JobTitle = Convert.ToString(data.GetValue("invoiceJobTitle") ?? ""),
                Phone = Convert.ToString(data.GetValue("invoicePhone") ?? ""),
            };
        }
        /// <summary>
        /// Get GroupCoordinator object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>GroupCoordinator Model</returns>
        private GroupCoordinator GetGroupCoordinator_School(IContent data)
        {
            return new GroupCoordinator()
            {
                Title = Convert.ToString(data.GetValue("title") ?? ""),
                FirstName = Convert.ToString(data.GetValue("firstName") ?? ""),
                SureName = Convert.ToString(data.GetValue("surename") ?? ""),
                Email = Convert.ToString(data.GetValue("email") ?? ""),
                Mobile = Convert.ToString(data.GetValue("mobile") ?? ""),
                DaytimeNumber = Convert.ToString(data.GetValue("daytimeNumber") ?? ""),
                SerialNumber = Convert.ToString(data.GetValue("groupCoordinatorSerialNumber") ?? "")
            };
        }

        private GroupCoordinator GetGroupCoordinator_Adult(IContent data)
        {
            return new GroupCoordinator()
            {
                Title = Convert.ToString(data.GetValue("title") ?? ""),
                FirstName = Convert.ToString(data.GetValue("firstName") ?? ""),
                SureName = Convert.ToString(data.GetValue("surename") ?? ""),
                Email = Convert.ToString(data.GetValue("email") ?? ""),
                Mobile = Convert.ToString(data.GetValue("mobile") ?? ""),
                DaytimeNumber = Convert.ToString(data.GetValue("daytimeNumber") ?? ""),
                SerialNumber = Convert.ToString(data.GetValue("groupCoordinatorSerialNumber") ?? ""),
                Address = Convert.ToString(data.GetValue("address") ?? ""),
                Suburb = Convert.ToString(data.GetValue("suburb") ?? ""),
                State = Convert.ToString(data.GetValue("state") ?? ""),
                Postcode = Convert.ToString(data.GetValue("postcode") ?? ""),
            };
        }

        private GroupCoordinator GetGroupCoordinator_University(IContent data)
        {
            return new GroupCoordinator()
            {
                Title = Convert.ToString(data.GetValue("title") ?? ""),
                FirstName = Convert.ToString(data.GetValue("firstName") ?? ""),
                SureName = Convert.ToString(data.GetValue("surename") ?? ""),
                Email = Convert.ToString(data.GetValue("email") ?? ""),
                Mobile = Convert.ToString(data.GetValue("mobile") ?? ""),
                DaytimeNumber = Convert.ToString(data.GetValue("daytimeNumber") ?? ""),
                SerialNumber = Convert.ToString(data.GetValue("groupCoordinatorSerialNumber") ?? ""),
                Address = Convert.ToString(data.GetValue("address") ?? ""),
                Suburb = Convert.ToString(data.GetValue("suburb") ?? ""),
                State = Convert.ToString(data.GetValue("state") ?? ""),
                Postcode = Convert.ToString(data.GetValue("postcode") ?? ""),
            };
        }

        /// <summary>
        /// Get EventCalendar object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Event Calendar Model</returns>
        private EventCalendar GetEventCalendar_School(IContent data)
        {
            return new EventCalendar()
            {
                title = Convert.ToString(data.GetValue("eventTitle") ?? ""),
                id = Convert.ToString(data.GetValue("eventId") ?? ""),
                start = Convert.ToString(data.GetValue("eventStart") ?? ""),
                end = Convert.ToString(data.GetValue("eventEnd") ?? ""),
                IsSameContact = Convert.ToBoolean(Convert.ToInt32(data.GetValue("isSameContact"))),
                GroupCoordinator = GetGroupCoordinator_School(data),
                Invoice = GetInvoice_School(data),
                AdditionalInfo = GetAdditionalinfo_School(data)
            };
        }

        private EventCalendar GetEventCalendar_Adult(IContent data)
        {
            return new EventCalendar()
            {
                title = Convert.ToString(data.GetValue("eventTitle") ?? ""),
                id = Convert.ToString(data.GetValue("eventId") ?? ""),
                start = Convert.ToString(data.GetValue("eventStart") ?? ""),
                end = Convert.ToString(data.GetValue("eventEnd") ?? ""),
                IsInvoiceOnly = Convert.ToBoolean(Convert.ToInt32(data.GetValue("isInvoiceOnly"))),
                GroupCoordinator = GetGroupCoordinator_Adult(data),
                Invoice = GetInvoice_Adult(data),
                AdditionalInfo = GetAdditionalinfo_Adult(data)
            };
        }

        private EventCalendar GetEventCalendar_University(IContent data)
        {
            return new EventCalendar()
            {
                title = Convert.ToString(data.GetValue("eventTitle") ?? ""),
                id = Convert.ToString(data.GetValue("eventId") ?? ""),
                start = Convert.ToString(data.GetValue("eventStart") ?? ""),
                end = Convert.ToString(data.GetValue("eventEnd") ?? ""),
                IsInvoiceOnly = Convert.ToBoolean(Convert.ToInt32(data.GetValue("isInvoiceOnly"))),
                GroupCoordinator = GetGroupCoordinator_University(data),
                Invoice = GetInvoice_University(data),
                AdditionalInfo = GetAdditionalinfo_University(data)
            };
        }

        /// <summary>
        /// Get AdditionalInfo object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>AdditionalInfo Model</returns>
        private AdditionalInfoModel GetAdditionalinfo_School(IContent data)
        {
            return new AdditionalInfoModel()
            {
                ContentKnowledge = Convert.ToString(data.GetValue("contentKnowledge") ?? ""),
                TotalCost = Convert.ToString(data.GetValue("totalCost") ?? ""),
                PerCost = Convert.ToString(data.GetValue("eventPriceStudent") ?? ""),
                AdditionalDetail = Convert.ToString(data.GetValue("additionalDetails") ?? ""),
                CafeRequire = Convert.ToInt32(data.GetValue("cafeRequirement") ?? 0) == 1
            };
        }

        private AdditionalInfoModel GetAdditionalinfo_Adult(IContent data)
        {
            return new AdditionalInfoModel()
            {
                TotalCost = Convert.ToString(data.GetValue("totalCost") ?? "")
            };
        }

        private AdditionalInfoModel GetAdditionalinfo_University(IContent data)
        {
            return new AdditionalInfoModel()
            {
                TotalCost = Convert.ToString(data.GetValue("totalCost") ?? ""),
                StaffTotalCost = Convert.ToString(data.GetValue("staffTotalCost") ?? "")
            };
        }
        #endregion




    }
}