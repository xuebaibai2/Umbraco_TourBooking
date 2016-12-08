using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class BookingDetailController : SurfaceController
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
        public PartialViewResult BookingDetail(string bookType, string id)
        {
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return PartialView("_Error");
            }

            ViewBag.parentUrl = CurrentPage.Parent.Url + "?id=" + id;

            if (bookType.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(id));
                if (school == null)
                {
                    return PartialView("_Error");
                }
                school.PreferredDate = GetDateTimeForInitial(school as BaseModel).ToString("dd/MM/yyyy");
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolBookingDetail.cshtml", school);
            }
            else if (bookType.Equals("Adult"))
            {
                AdultModel adult = contentController.GetAdultModelById(Convert.ToInt32(id));
                if (adult == null)
                {
                    return PartialView("_Error");
                }
                adult.PreferredDate = GetDateTimeForInitial(adult as BaseModel).ToString("dd/MM/yyyy");
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultBookingDetail.cshtml", adult);
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
        [ValidateAntiForgeryToken]
        public ActionResult PostBooking_School(SchoolModel school)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostBooking_School(school);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("id", school.Id.ToString());
                return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("SchoolAdditionalDetail", CurrentPage), routeValues);
            }
            return CurrentUmbracoPage();
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostBooking_Adult(AdultModel adult)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostBooking_Adult(adult);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("id", adult.Id.ToString());
                return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("AdultInvoice", CurrentPage), routeValues);
            }
            return CurrentUmbracoPage();
        }

        private DateTime GetDateTimeForInitial(BaseModel viewModel)
        {
            return DateTime.ParseExact(viewModel.PreferredDate, "MM/d/yyyy hh:mm:ss tt", new System.Globalization.CultureInfo("en-AU"), System.Globalization.DateTimeStyles.None);
        }
	}
}