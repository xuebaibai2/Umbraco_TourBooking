using SYJMA.Umbraco.Models.ErrorModel;
using SYJMA.Umbraco.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class ErrorPageController : SurfaceController
    {
        public PartialViewResult PageNotFound(IErrorPage page)
        {
            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            if (page.ErrorCode.Equals("404"))
            {
                PageNotFound model = new PageNotFound()
                {
                    ErrorTitle = page.ErrorTitle,
                    ErrorCode = page.ErrorCode,
                    ErrorDescription = page.ErrorDescription
                };
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ERROR_FOLDER + "_404.cshtml", model);
            }
            return PartialView("_Error");
        }
    }
}