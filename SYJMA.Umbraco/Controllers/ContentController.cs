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
            else
            {
                int recordId = Convert.ToInt32(data.GetValue("year"));
                int subjectId = Convert.ToInt32(data.GetValue("subjectArea"));

                string yearValue = dataType.GetDropdownListValue(recordId, CONSTID.SCHOOL_YEAR_DROPDOWNLIST_NAME);
                string subjectArea = dataType.GetDropdownListValue(subjectId, CONSTID.SCHOOL_SUBJECT_DROPDOWNLIST_NAME);
                EventCalendar c = new EventCalendar();
                c.title = Convert.ToString(data.GetValue("eventTitle"));
                c.id = Convert.ToString(data.GetValue("eventId"));
                c.start = Convert.ToString(data.GetValue("eventStart"));
                c.end = Convert.ToString(data.GetValue("eventEnd"));

                return new SchoolModel()
                {
                    Id = Convert.ToInt32(data.GetValue("recordId")),
                    Year = yearValue,
                    SchoolName = Convert.ToString(data.GetValue("nameOfSchool")),
                    PreferredDate = Convert.ToString(data.GetValue("preferredDateSchool")),
                    SubjectArea = subjectArea,
                    StudentsNumber = Convert.ToInt32(data.GetValue("numberOfStudents")),
                    StaffNumber = Convert.ToInt32(data.GetValue("numberOfStaff")),
                    Comments = Convert.ToString(data.GetValue("comments")),
                    Event =c
                };
            }
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