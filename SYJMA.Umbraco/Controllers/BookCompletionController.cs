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
    public class BookCompletionController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();
        private JSONDataController jsonDataController = new JSONDataController();

        public PartialViewResult BookCompletion(string type, string mainBookingId)
        {
            int result;
            if (!Int32.TryParse(mainBookingId, out result))
            {
                return PartialView("_Error");
            }
            if (type.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(mainBookingId));
                if (school == null)
                {
                    return PartialView("_Error");
                }
                List<SchoolModel> schoolList = new List<SchoolModel>() ;
                schoolList.Add(school);
                var childIdList = Services.ContentService.GetChildren(Convert.ToInt32(mainBookingId)).Select(x=>x.Id).ToList();
                if (childIdList == null)
                {
                    return PartialView("_Error");
                }
                foreach (int id in childIdList)
                {
                    schoolList.Add(contentController.GetSchoolModelById(id));
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_SCHOOL_FOLDER + "_SchoolBookCompletion.cshtml", schoolList);
            }
            else if (type.Equals("Adult"))
            {
                AdultModel adult = contentController.GetAdultModelById(Convert.ToInt32(mainBookingId));
                if (adult == null)
                {
                    return PartialView("_Error");
                }
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultBookCompletion.cshtml", adult);
            }
            else if (type.Equals("University"))
            {

            }


            return null;
        }
	}
}