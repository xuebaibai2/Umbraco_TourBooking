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
    public class ConfirmationPanelController : SurfaceController
    {
        private ContentController contentController = new ContentController();

        /// <summary>
        /// Render Partial View based on the bookType and book model id
        /// </summary>
        /// <param name="bookType"></param>
        /// <param name="id"></param>
        /// <returns>Partial view based on the booktype and the model</returns>
        public PartialViewResult ConfirmationPanel(string bookType, string id)
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
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolConfirmPanel.cshtml", school);
            }
            else if (bookType.Equals(TOURCATEGORY.ADULT))
            {
                AdultModel adult = contentController.GetModelById_Adult(Convert.ToInt32(id));
                if (adult == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultConfirmPanel.cshtml", adult);
            }
            else if (bookType.Equals(TOURCATEGORY.UNIVERSITY))
            {
                UniversityModel uni = contentController.GetModelById_University(Convert.ToInt32(id));
                if (uni == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_UNIVERSITY_FOLDER + "_UniConfirmPanel.cshtml", uni);
            }
            return null;
        }

        /// <summary>
        /// Retrieve data from privious page, insert data to umbraco and redirect to next page
        /// </summary>
        /// <param name="school"></param>
        /// <returns>Redirect to next page</returns>
        [ValidateAntiForgeryToken]
        public ActionResult PostConfirm_School(SchoolModel school)
        {
            school = contentController.GetModelById_School(school.Id);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDByName("SchoolDetail"), routeValues);
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostConfirm_Adult(AdultModel adult)
        {
            adult = contentController.GetModelById_Adult(adult.Id);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", adult.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDByName("AdultDetail"), routeValues);
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostConfirm_University(UniversityModel uni)
        {
            uni = contentController.GetModelById_University(uni.Id);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", uni.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDByName("UniversityDetail"), routeValues);
        }
	}
}