using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class ContentController : SurfaceController
    {
        DataTypeController dataType = new DataTypeController();

        /// <summary>
        /// Get School object from Umbraco record  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>School Model</returns>
        public SchoolModel GetSchoolModelById(int id)
        {
            var data = ApplicationContext.Services.ContentService.GetById(id);
            if (data == null)
            {
                return null;
            }
            else if (!data.Parent().Name.Equals(CONSTVALUE.SCHOOL_VISITS_CONTENT))
            {
                return null;
            }
            else
            {
                EventCalendar eventCalendar = GetEventCalendar(data);
                List<Attendee> attendeeList = GetAttendeeList(data);
                SchoolModel model = new SchoolModel()
                {
                    Id = Convert.ToInt32(data.GetValue("recordId")),
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
        }

        public void CreateNewSchoolModel(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.CreateContent(school.SchoolName + " - " + school.SubjectArea, CurrentPage.Id, "School");

            schoolRecord.SetValue("schoolSerialNumber", school.SerialNumber);
            schoolRecord.SetValue("nameOfSchool", school.SchoolName);
            schoolRecord.SetValue("year", school.Year);
            schoolRecord.SetValue("preferredDateSchool", GetDateTime(school));
            schoolRecord.SetValue("subjectArea", school.SubjectArea);
            schoolRecord.SetValue("numberOfStudents", school.StudentsNumber);
            schoolRecord.SetValue("numberOfStaff", school.StaffNumber);
            schoolRecord.SetValue("comments", school.Comments);
            Services.ContentService.SaveAndPublishWithStatus(schoolRecord);
            school.Id = schoolRecord.Id;
            schoolRecord.SetValue("recordId", school.Id);
            Services.ContentService.Save(schoolRecord);
        }

        /// <summary>
        /// Convert string format of datetime to DateTime datatype
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Preferred booking date with datetime format</returns>
        private DateTime GetDateTime(BaseModel viewModel)
        {
            return Convert.ToDateTime(viewModel.PreferredDate, new System.Globalization.CultureInfo("en-AU", true));
        }

        /// <summary>
        /// Search Content ID by content name from parent path
        /// </summary>
        /// <param name="contentName"></param>
        /// <param name="page">The current published content</param>
        /// <returns>Content ID</returns>
        public int GetContentIDFromParent(string contentName, IPublishedContent page)
        {
            return Services.ContentService.GetChildren(page.Parent.Id)
                .First(x => x.Name == contentName).Id;
        }

        /// <summary>
        /// Get Content ID based on content name
        /// </summary>
        /// <param name="contentName"></param>
        /// <param name="page">The current published content</param>
        /// <returns>Content ID</returns>
        public int GetContentIDFromSelf(string contentName, IPublishedContent page)
        {
            return Services.ContentService.GetChildren(page.Id)
                .First(x => x.Name == contentName).Id;
        }

        #region 'Private Region'

        private List<Attendee> GetAttendeeList(IContent data)
        {
            List<Attendee> temp = new List<Attendee>();
            temp.Add(new Attendee()
            {
                ID = Convert.ToString(data.GetValue("studentAttendeeTypeID") ?? ""),
                Cost = float.Parse(Convert.ToString(data.GetValue("eventPriceStudent") ?? 0)),
                Type = ATTENDEETYPE.ATTENDEETYPE_STUDENT
            });
            temp.Add(new Attendee()
            {
                ID = Convert.ToString(data.GetValue("staffAttendeeTypeID") ?? ""),
                Cost = float.Parse(Convert.ToString(data.GetValue("eventPriceStaff") ?? 0)),
                Type = ATTENDEETYPE.ATTENDEETYPE_STAFF
            });
            return temp;
        }
        /// <summary>
        /// Get Invoice object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Invoice Model</returns>
        private Invoice GetInvoice(IContent data)
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

        /// <summary>
        /// Get GroupCoordinator object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>GroupCoordinator Model</returns>
        private GroupCoordinator GetGroupCoordinator(IContent data)
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

        /// <summary>
        /// Get EventCalendar object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Event Calendar Model</returns>
        private EventCalendar GetEventCalendar(IContent data)
        {
            return new EventCalendar()
            {
                title = Convert.ToString(data.GetValue("eventTitle") ?? ""),
                id = Convert.ToString(data.GetValue("eventId") ?? ""),
                start = Convert.ToString(data.GetValue("eventStart") ?? ""),
                end = Convert.ToString(data.GetValue("eventEnd") ?? ""),
                //studentPrice = float.Parse(Convert.ToString(data.GetValue("eventPriceStudent") ?? 0)),
                GroupCoordinator = GetGroupCoordinator(data),
                Invoice = GetInvoice(data),
                AdditionalInfo = GetAdditionalinfo(data)
            };
        }

        /// <summary>
        /// Get AdditionalInfo object from Umbraco record 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>AdditionalInfo Model</returns>
        private AdditionalInfoModel GetAdditionalinfo(IContent data)
        {
            return new AdditionalInfoModel()
            {
                ContentKnowledge = Convert.ToString(data.GetValue("contentKnowledge") ?? ""),
                TotalCost = Convert.ToString(data.GetValue("totalCost") ?? ""),
                PerCost = Convert.ToString(data.GetValue("perCost") ?? ""),
                AdditionalDetail = Convert.ToString(data.GetValue("additionalDetails") ?? ""),
                CafeRequire = Convert.ToInt32(data.GetValue("cafeRequirement") ?? 0) == 1
            };
        }

        #endregion

    }
}