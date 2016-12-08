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
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();

        /// <summary>
        /// Render Partial View based on the bookType and book model id
        /// </summary>
        /// <param name="bookType"></param>
        /// <param name="id"></param>
        /// <returns>Partial view based on the booktype and the model</returns>
        public PartialViewResult ConfirmationPanel(string bookType, string id)
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
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolConfirmPanel.cshtml", school);
            }
            else if (bookType.Equals("Adult"))
            {
                AdultModel adult = contentController.GetAdultModelById(Convert.ToInt32(id));
                if (adult == null)
                {
                    return PartialView("_Error");
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultConfirmPanel.cshtml", adult);
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
        public ActionResult PostConfirm_School(SchoolModel school)
        {
            school = contentController.GetSchoolModelById(school.Id);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("SchoolDetail", CurrentPage), routeValues);
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostConfirm_Adult(AdultModel adult)
        {
            adult = contentController.GetAdultModelById(adult.Id);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", adult.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("AdultDetail", CurrentPage), routeValues);
        }
	}
}