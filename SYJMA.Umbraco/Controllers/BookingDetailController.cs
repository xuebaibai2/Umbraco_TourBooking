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
            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
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
                school.PreferredDate = GetDateTimeForInitial(school as BaseModel).ToString("dd/MM/yyyy");
                school.Event.Invoice.Phone = "0";
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolBookingDetail.cshtml", school);
            }
            else if (bookType.Equals(TOURCATEGORY.ADULT))
            {
                AdultModel adult = contentController.GetModelById_Adult(Convert.ToInt32(id));
                if (adult == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                adult.PreferredDate = GetDateTimeForInitial(adult as BaseModel).ToString("dd/MM/yyyy");
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultBookingDetail.cshtml", adult);
            }
            else if (bookType.Equals(TOURCATEGORY.UNIVERSITY))
            {
                UniversityModel uni = contentController.GetModelById_University(Convert.ToInt32(id));
                if (uni == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                uni.PreferredDate = GetDateTimeForInitial(uni as BaseModel).ToString("dd/MM/yyyy");
                return PartialView(CONSTVALUE.PARTIAL_VIEW_UNIVERSITY_FOLDER + "_UniBookingDetail.cshtml", uni);
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
                return RedirectToUmbracoPage(contentController.GetContentIDByName("SchoolAdditionalDetail"), routeValues);
            }
            return CurrentUmbracoPage();
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostBooking_Adult(AdultModel adult)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostBooking_Adult(adult);
                adult = contentController.GetModelById_Adult(adult.Id);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("id", adult.Id.ToString());

                if (adult.Event.IsInvoiceOnly)
                {
                    return RedirectToUmbracoPage(contentController.GetContentIDByName("AdultInvoice"), routeValues);
                }
                else
                {
                    return RedirectToUmbracoPage(contentController.GetContentIDByName("AdultPayment"), routeValues);
                }
            }
            return CurrentUmbracoPage();
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostBooking_University(UniversityModel uni)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostBooking_University(uni);
                uni = contentController.GetModelById_University(uni.Id);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("id", uni.Id.ToString());

                if (uni.Event.IsInvoiceOnly)
                {
                    return RedirectToUmbracoPage(contentController.GetContentIDByName("UniversityInvoice"), routeValues);
                }
                else
                {
                    return RedirectToUmbracoPage(contentController.GetContentIDByName("UniversityPayment"), routeValues);
                }
            }
            return CurrentUmbracoPage();
        }
        private DateTime GetDateTimeForInitial(BaseModel viewModel)
        {
            return DateTime.ParseExact(viewModel.PreferredDate, "M/d/yyyy hh:mm:ss tt", new System.Globalization.CultureInfo("en-AU"), System.Globalization.DateTimeStyles.None);
        }
    }
}