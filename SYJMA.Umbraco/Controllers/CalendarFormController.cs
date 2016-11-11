using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
	}
}