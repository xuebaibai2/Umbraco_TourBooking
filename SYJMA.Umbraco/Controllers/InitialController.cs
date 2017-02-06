using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using umbraco;
using umbraco.NodeFactory;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class InitialController : SurfaceController
    {
        private ContentController contentController = new ContentController();
        private JSONDataController jsonDataController = new JSONDataController();

        

        /// <summary>
        /// Render partial view for Initial Identification Page
        /// </summary>
        /// <param name="bookType">The type of booking School / Adult / University</param>
        /// <returns>Partial view with Model</returns>
        public PartialViewResult InitialIdentification(string bookType)
        {
            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            if (bookType.Equals(TOURCATEGORY.SCHOOL))
            {
                Session["idList"] = new List<int>();
                SchoolModel school = new SchoolModel();
                school.SubjectList = jsonDataController.GetJsonData_SubjectAreaList();
                school.YearList = jsonDataController.GetJsonData_YearGroupList();
                school.type = bookType;

                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolVisit.cshtml", school);
            }
            else if (bookType.Equals(TOURCATEGORY.ADULT))
            {
                AdultModel adult = new AdultModel();
                adult.type = bookType;
                adult.ProgramList = jsonDataController.GetJsonData_EventNameList(TOURCATEGORY.ADULT);
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultVisit.cshtml", adult);
            }
            else if (bookType.Equals(TOURCATEGORY.UNIVERSITY))
            {
                UniversityModel uni = new UniversityModel();
                uni.type = bookType;
                uni.ProgramList = jsonDataController.GetJsonData_EventNameList(TOURCATEGORY.UNIVERSITY);
                return PartialView(CONSTVALUE.PARTIAL_VIEW_UNIVERSITY_FOLDER + "_UniVisit.cshtml", uni);
            }
            // Return page not found
            return null;
        }

        #region School

        /// <summary>
        /// Receive post data form from School partialview and save into School Visits content as a record
        /// </summary>
        /// <param name="school"></param>
        /// <returns>Redirect to page</returns>
        [ValidateAntiForgeryToken]
        public ActionResult PostInitialPage_School(SchoolModel school)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostInitialPage_School(school, CurrentPage);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("id", school.Id.ToString());
                return RedirectToUmbracoPage(contentController.GetContentIDByName("School Calendar Form"), routeValues);
            }
            return CurrentUmbracoPage();
        }

       
        public PartialViewResult SubtourInitialIdentification(string bookType, string id)
        {
            SchoolModel school = new SchoolModel();
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return PartialView("_Error");
            }
            school = contentController.GetModelById_School(Convert.ToInt32(id));
            school.SubjectList = jsonDataController.GetJsonData_SubjectAreaList();
            school.YearList = jsonDataController.GetJsonData_YearGroupList();
            school.PreferredDate = GetDateTimeForInitial(school as BaseModel).ToString("dd/MM/yyyy");
            contentController.CreateNewSchoolModel(school);
            if (bookType.Equals("School"))
            {
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolSubtourBooking.cshtml", school);
            }
            return null;
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostSubTourInitialPage_School(SchoolModel school)
        {
            contentController.SetPostSubTourInitialPage_School(school);
            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());
            return RedirectToUmbracoPage(contentController.GetContentIDByName("School Calendar Form"), routeValues);
        }

        #endregion

        #region Adult
        /// <summary>
        /// Receive post data form from Adult partialview and save into Adult Visits content as a record
        /// </summary>
        /// <param name="adult"></param>
        /// <returns>Redirect to page</returns>
        [ValidateAntiForgeryToken]
        public ActionResult PostInitialPage_Adult(AdultModel adult)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostInitialPage_Adult(adult, CurrentPage);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("id", adult.Id.ToString());
                return RedirectToUmbracoPage(contentController.GetContentIDByName("Adult Calendar Form"), routeValues);
            }
            return CurrentUmbracoPage();
        }

        #endregion

        #region University
        [ValidateAntiForgeryToken]
        public ActionResult PostInitialPage_University(UniversityModel uni)
        {
            if (ModelState.IsValid)
            {
                contentController.SetPostInitialPage_University(uni, CurrentPage);
                NameValueCollection routeValues = new NameValueCollection();
                routeValues.Add("id", uni.Id.ToString());
                return RedirectToUmbracoPage(contentController.GetContentIDByName("University Calendar Form"), routeValues);
            }
            return CurrentUmbracoPage();
        }
        #endregion

        #region 'Private Region'

        private DateTime GetDateTimeForInitial(BaseModel viewModel)
        {
            return DateTime.ParseExact(viewModel.PreferredDate, "MM/d/yyyy hh:mm:ss tt", new System.Globalization.CultureInfo("en-AU"), System.Globalization.DateTimeStyles.None);
        }

        #endregion
    }
}