using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;
using SYJMA.Umbraco.Models.ErrorModel;
namespace SYJMA.Umbraco.Controllers
{
    public class BookCompletionController : SurfaceController
    {
        private ContentController contentController = new ContentController();
        private JSONDataController jsonDataController = new JSONDataController();

        public PartialViewResult BookCompletion(string type, string mainBookingId)
        {
            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            int result;
            if (!Int32.TryParse(mainBookingId, out result))
            {
                return contentController.GetPartialView_PageNotFound();
            }
            if (type.Equals(TOURCATEGORY.SCHOOL))
            {
                SchoolModel school = contentController.GetModelById_School(Convert.ToInt32(mainBookingId));
                if (school == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                List<SchoolModel> schoolList = new List<SchoolModel>() ;
                schoolList.Add(school);
                var childIdList = Services.ContentService.GetChildren(Convert.ToInt32(mainBookingId)).Select(x=>x.Id).ToList();
                if (childIdList == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                foreach (int id in childIdList)
                {
                    schoolList.Add(contentController.GetModelById_School(id));
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolBookCompletion.cshtml", schoolList);
            }
            else if (type.Equals(TOURCATEGORY.ADULT))
            {
                AdultModel adult = contentController.GetModelById_Adult(Convert.ToInt32(mainBookingId));
                if (adult == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultBookCompletion.cshtml", adult);
            }
            else if (type.Equals(TOURCATEGORY.UNIVERSITY))
            {
                UniversityModel uni = contentController.GetModelById_University(Convert.ToInt32(mainBookingId));
                if (uni == null)
                {
                    return contentController.GetPartialView_PageNotFound();
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_UNIVERSITY_FOLDER + "_UniBookCompletion.cshtml", uni);
            }
            return null;
        }
	}
}