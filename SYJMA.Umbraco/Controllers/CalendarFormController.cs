using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SYJMA.Umbraco.Models;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class CalendarFormController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();

        public PartialViewResult CalendarForm(string bookType, string id)
        {
            if (bookType.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(id))!= null 
                    ? contentController.GetSchoolModelById(Convert.ToInt32(id)) : null;
                if (school == null)
                {
                    return PartialView("_Error");
                }
                school.ProgramList = dataTypeController.GetSchoolProgramList();
                return PartialView("_SchoolCalendar", school);
            }
            else if (bookType.Equals("Adult"))
            {
                
            }
            else if (bookType.Equals("University"))
            {

            }
            return null;
        }

        public ActionResult PostCalendarForm_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("eventTitle",school.Event.title);
            schoolRecord.SetValue("eventId",school.Event.id);
            schoolRecord.SetValue("eventStart",school.Event.start);
            schoolRecord.SetValue("eventEnd",school.Event.end);
            Services.ContentService.Save(schoolRecord);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("SchoolConfirm", CurrentPage), routeValues);
        }


        
	}
}