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

        public SchoolModel GetSchoolModelById(int id)
        {
            var data = ApplicationContext.Services.ContentService.GetById(id);
            if (data == null)
            {
                return null;
            }
            else if (!data.Parent().Name.Equals(CONSTID.SCHOOL_VISITS_CONTENT))
            {
                return null;
            }
            else
            {
                int recordId = Convert.ToInt32(data.GetValue("year"));
                int subjectId = Convert.ToInt32(data.GetValue("subjectArea"));

                string yearValue = dataType.GetDropdownListValue(recordId, CONSTID.SCHOOL_YEAR_DROPDOWNLIST_NAME);
                string subjectArea = dataType.GetDropdownListValue(subjectId, CONSTID.SCHOOL_SUBJECT_DROPDOWNLIST_NAME);

                EventCalendar eventCalendar = GetEventCalendar(data);

                SchoolModel model = new SchoolModel()
                {
                    Id = Convert.ToInt32(data.GetValue("recordId")),
                    Year = yearValue,
                    SchoolName = Convert.ToString(data.GetValue("nameOfSchool")),
                    PreferredDate = Convert.ToString(data.GetValue("preferredDateSchool")),
                    SubjectArea = subjectArea,
                    StudentsNumber = Convert.ToInt32(data.GetValue("numberOfStudents")),
                    StaffNumber = Convert.ToInt32(data.GetValue("numberOfStaff")),
                    Comments = Convert.ToString(data.GetValue("comments")),
                    Event = eventCalendar
                };
                return model;
            }
        }

        private Invoice GetInvoice(IContent data)
        {
            return new Invoice()
                            {
                                Title = Convert.ToString(data.GetValue("invoiceTitle") ?? ""),
                                FirstName = Convert.ToString(data.GetValue("invoiceFirstName") ?? ""),
                                SureName = Convert.ToString(data.GetValue("invoiceSurename") ?? ""),
                                Email = Convert.ToString(data.GetValue("invoiceEmail") ?? "")
                            };
        }

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
                Invoice = GetInvoice(data)
            };
        }

        private EventCalendar GetEventCalendar(IContent data)
        {
            return new EventCalendar()
            {
                title = Convert.ToString(data.GetValue("eventTitle") ?? ""),
                id = Convert.ToString(data.GetValue("eventId") ?? ""),
                start = Convert.ToString(data.GetValue("eventStart") ?? ""),
                end = Convert.ToString(data.GetValue("eventEnd") ?? ""),
                studentPrice = float.Parse(Convert.ToString(data.GetValue("eventPriceStudent") ?? 0)),
                GroupCoordinator = GetGroupCoordinator(data),
                AdditionalInfo = GetAdditionalinfo(data)
            };
        }

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

        public int GetContentIDFromParent(string contentName, IPublishedContent page)
        {
            return Services.ContentService.GetChildren(page.Parent.Id)
                .First(x => x.Name == contentName).Id;
        }

        public int GetContentIDFromSelf(string contentName, IPublishedContent page)
        {
            return Services.ContentService.GetChildren(page.Id)
                .First(x => x.Name == contentName).Id;
        }

    }
}