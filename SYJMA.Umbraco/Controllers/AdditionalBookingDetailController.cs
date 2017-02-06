using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Models.API;
using SYJMA.Umbraco.Utility;
using umbraco;
using umbraco.NodeFactory;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;
using SYJMA.Umbraco.Models.ErrorModel;

namespace SYJMA.Umbraco.Controllers
{
    public class AdditionalBookingDetailController : SurfaceController
    {
        private ContentController contentController = new ContentController();
        private JSONDataController jsonDataController = new JSONDataController();

        /// <summary>
        /// Render Partial View based on the bookType and book model id
        /// </summary>
        /// <param name="bookType"></param>
        /// <param name="id"></param>
        /// <returns>Partial view based on the booktype and the model</returns>
        public PartialViewResult AdditionalBookingDetail(string bookType, string id)
        {
            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return contentController.GetPartialView_PageNotFound();
            }

            if (bookType.Equals(TOURCATEGORY.SCHOOL))
            {
                SchoolModel school = contentController.GetModelById_School(Convert.ToInt32(id));
                if (school == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }

                school.SubTourIDList = Session["idList"] as List<int>;
                if (school.SubTourIDList != null)
                {
                    school.SubTourIDList.Add(int.Parse(id));
                    Session["idList"] = school.SubTourIDList;
                }
                else
                {
                    TempData["SessionTimeout"] = "Your session has timed out. Please try to book again.";
                }
                ViewBag.parentUrl = CurrentPage.Parent.Url + "?id=" + id;
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolAdditionalBookingDetail.cshtml", school);
            }
            else if (bookType.Equals(TOURCATEGORY.ADULT))
            {

            }
            else if (bookType.Equals(TOURCATEGORY.UNIVERSITY))
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
        public ActionResult PostAdditionalBooking_School(SchoolModel school, string command)
        {
            //If the submit button is booking another school tour then redirect to InitialController
            if (command.Equals("subtour"))
            {
                NameValueCollection subTourRouteValues = new NameValueCollection();
                subTourRouteValues.Add("id", school.MainBookingID.ToString());
                Node node = uQuery.GetNodesByName("School Visits").FirstOrDefault();
                return RedirectToUmbracoPage(node.Id, subTourRouteValues);
            }

            school = contentController.GetModelById_School(school.Id);
            //Create new contact with primary category as schools and contacttype as organisation
            string schoolSerialNumber = jsonDataController.CreateNewOrganisationContactOnThankQ<SchoolModel>(school);

            school.SubTourIDList = Session["idList"] as List<int>;
            if (school.SubTourIDList == null)
            {
                TempData["SessionTimeout"] = "Your session has timed out. Please try to book again.";
                return CurrentUmbracoPage();
            }

            foreach (int id in school.SubTourIDList)
            {
                SchoolModel tempSchool = contentController.GetModelById_School(id);
                tempSchool.SerialNumber = schoolSerialNumber;
                //Create new contact for Group Coordinator and Invoicee on ThankQ DB
                jsonDataController.CreateNewContactOnThankQ<SchoolModel>(tempSchool);
                //Create new Tour Booking Record on ThankQ DB
                //tempSchool.TourBookingID = jsonDataController.CreateNewTourBookingOnThankQ(tempSchool);
                tempSchool.TourBookingID = jsonDataController.CreateNewTourBookingOnThankQ<SchoolModel>(tempSchool);
                //Create new Attendee Summary on ThankQ DB
                jsonDataController.CreateNewTourBookingAttendeeSummaryOnThankQ<SchoolModel>(tempSchool);
                //Save booking record on Umbraco CMS
                contentController.SetPostAdditionalBooking_School(tempSchool);
            }
            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("mainBookingId", school.MainBookingID.ToString());
            routeValues.Add("type", TOURCATEGORY.SCHOOL);
            if (school.Event.AdditionalInfo.CafeRequire)
            {
                //Do Something About Cafe Catering Sending Email?
            }
            return RedirectToUmbracoPage(CONSTVALUE.BOOK_COMPLETION_CONTENT_ID, routeValues);
        }
    }
}