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

        public PartialViewResult BookCompletion(string id,string type)
        {
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return PartialView("_Error");
            }
            if (type.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(id));
                if (school == null)
                {
                    return PartialView("_Error");
                }
                return PartialView("~/Views/Partials/School/_SchoolBookCompletion.cshtml", school);
            }
            else if (type.Equals("Adult"))
            {

            }
            else if (type.Equals("University"))
            {

            }


            return null;
        }
	}
}