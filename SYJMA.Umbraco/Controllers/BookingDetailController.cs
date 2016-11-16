using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class BookingDetailController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();

        public PartialViewResult BookingDetail(string bookType, string id)
        {
            if (bookType.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(id)) != null
                    ? contentController.GetSchoolModelById(Convert.ToInt32(id)) : null;
                if (school == null)
                {
                    return PartialView("_Error");
                }
                return PartialView("~/Views/Partials/School/_SchoolBookingDetail.cshtml", school);
            }
            else if (bookType.Equals("Adult"))
            {

            }
            else if (bookType.Equals("University"))
            {

            }
            return null;
        }

        public ActionResult PostBooking_School(SchoolModel school)
        {
            school = contentController.GetSchoolModelById(school.Id);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("", CurrentPage), routeValues);
        }
	}
}