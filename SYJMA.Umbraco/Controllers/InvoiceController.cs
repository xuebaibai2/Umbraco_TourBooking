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
    public class InvoiceController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();

        public PartialViewResult Invoice(string bookType, string id)
        {
            //If id is not an Integer type will redirect to error page
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
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolInvoice.cshtml", school);
            }
            else if (bookType.Equals("Adult"))
            {
                AdultModel adult = contentController.GetAdultModelById(Convert.ToInt32(id));
                if (adult == null)
                {
                    return PartialView("_Error");
                }
                adult.PreferredDate = GetDateTimeForInitial(adult as BaseModel).ToString("dd/MM/yyyy");
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultInvoice.cshtml", adult);
            }
            else if (bookType.Equals("University"))
            {

            }
            return null;
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostInvoice_Adult(AdultModel adult)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostInvoice_Adult(adult);
                adult = contentController.GetAdultModelById(adult.Id);

                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("mainBookingId", adult.Id.ToString());
                routeValues.Add("type", "Adult");

                return RedirectToUmbracoPage(CONSTVALUE.BOOK_COMPLETION_CONTENT_ID, routeValues);
            }
            return CurrentUmbracoPage();
        }


        private DateTime GetDateTimeForInitial(BaseModel viewModel)
        {
            return DateTime.ParseExact(viewModel.PreferredDate, "MM/d/yyyy hh:mm:ss tt", new System.Globalization.CultureInfo("en-AU"), System.Globalization.DateTimeStyles.None);
        }
	}
}