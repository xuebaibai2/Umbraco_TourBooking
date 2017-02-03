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
        private ContentController contentController = new ContentController();
        private JSONDataController jsonDataController = new JSONDataController();

        public PartialViewResult Invoice(string bookType, string id)
        {
            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            //If id is not an Integer type will redirect to error page
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return contentController.GetPartialView_PageNotFound();
            }

            ViewBag.parentUrl = CurrentPage.Parent.Url + "?id=" + id;

            if (bookType.Equals(TOURCATEGORY.SCHOOL))
            {
                SchoolModel school = contentController.GetModelById_School(Convert.ToInt32(id));
                if (school == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolInvoice.cshtml", school);
            }
            else if (bookType.Equals(TOURCATEGORY.ADULT))
            {
                AdultModel adult = contentController.GetModelById_Adult(Convert.ToInt32(id));
                if (adult == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                adult.PreferredDate = GetDateTimeForInitial(adult as BaseModel).ToString("dd/MM/yyyy");
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultInvoice.cshtml", adult);
            }
            else if (bookType.Equals(TOURCATEGORY.UNIVERSITY))
            {
                UniversityModel uni = contentController.GetModelById_University(Convert.ToInt32(id));
                if (uni == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                uni.PreferredDate = GetDateTimeForInitial(uni as BaseModel).ToString("dd/MM/yyyy");
                return PartialView(CONSTVALUE.PARTIAL_VIEW_UNIVERSITY_FOLDER + "_UniInvoice.cshtml", uni);
            }
            return contentController.GetPartialView_PageNotFound();
        }
        
        [ValidateAntiForgeryToken]
        public ActionResult PostInvoice_Adult(AdultModel adult)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostInvoice_Adult(adult);
                adult = contentController.GetModelById_Adult(adult.Id);

                //Save Group Coordinator and Invoicee on ThankQ BD
                jsonDataController.CreateNewContactOnThankQ<AdultModel>(adult);
                //Create new Tour Booking Record on ThankQ DB
                adult.TourBookingID = jsonDataController.CreateNewTourBookingOnThankQ<AdultModel>(adult);
                //Create new Attendee Summary on ThankQ DB
                jsonDataController.CreateNewTourBookingAttendeeSummaryOnThankQ<AdultModel>(adult);
                //Save booking record on Umbraco CMS
                contentController.SetPostAdditionalBooking_Adult(adult);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("mainBookingId", adult.Id.ToString());
                routeValues.Add("type", TOURCATEGORY.ADULT);

                return RedirectToUmbracoPage(CONSTVALUE.BOOK_COMPLETION_CONTENT_ID, routeValues);
            }
            return CurrentUmbracoPage();
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostInvoice_University(UniversityModel uni)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostInvoice_University(uni);
                uni = contentController.GetModelById_University(uni.Id);

                //Create new contact with primary category as schools and contacttype as organisation
                uni.SerialNumber = jsonDataController.CreateNewOrganisationContactOnThankQ<UniversityModel>(uni);
                //Save Group Coordinator and Invoicee on ThankQ BD
                jsonDataController.CreateNewContactOnThankQ<UniversityModel>(uni);
                //Create new Tour Booking Record on ThankQ DB
                uni.TourBookingID = jsonDataController.CreateNewTourBookingOnThankQ<UniversityModel>(uni);
                //Create new Attendee Summary on ThankQ DB
                jsonDataController.CreateNewTourBookingAttendeeSummaryOnThankQ<UniversityModel>(uni);
                //Save booking record on Umbraco CMS
                contentController.SetPostAdditionalBooking_University(uni);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("mainBookingId", uni.Id.ToString());
                routeValues.Add("type", TOURCATEGORY.UNIVERSITY);

                return RedirectToUmbracoPage(CONSTVALUE.BOOK_COMPLETION_CONTENT_ID, routeValues);
            }
            return CurrentUmbracoPage();
        }

        private DateTime GetDateTimeForInitial(BaseModel viewModel)
        {
            return DateTime.ParseExact(viewModel.PreferredDate, "M/d/yyyy hh:mm:ss tt", new System.Globalization.CultureInfo("en-AU"), System.Globalization.DateTimeStyles.None);
        }
	}
}