﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class ConfirmationPanelController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();

        public PartialViewResult ConfirmationPanel(string bookType, string id)
        {
            if (bookType.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(id)) != null 
                    ? contentController.GetSchoolModelById(Convert.ToInt32(id)) : null;
                if (school == null)
                {
                    return PartialView("_Error");
                }
                return PartialView("_SchoolConfirmPanel", school);
            }
            else if (bookType.Equals("Adult"))
            {

            }
            else if (bookType.Equals("University"))
            {

            }
            return null;
        }

        public ActionResult PostConfirm_School(SchoolModel school)
        {
            school = contentController.GetSchoolModelById(school.Id);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("SchoolDetail", CurrentPage), routeValues);
        }
	}
}