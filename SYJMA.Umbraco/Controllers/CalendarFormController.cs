using System;
using System.Collections.Generic;
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
        private DataTypeController DataType = new DataTypeController();

        public PartialViewResult CalendarForm(string bookType)
        {
            if (bookType.Equals("School"))
            {
                SchoolModel school = new SchoolModel();
                school.ProgramList = DataType.GetSchoolProgramRadioBtnList();
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
            return RedirectToCurrentUmbracoPage();
        }

        private const string API_HOST = "http://demo2054938.mockable.io/";
        public ActionResult GetJsonData(string eventName)
        {
            var jsonResult = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    jsonResult = wc.DownloadString(API_HOST + eventName);
                }
                catch (Exception)
                {
                    return null;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<EventCalendar>>(jsonResult);
                return Json(result);
            }
        }
	}
}