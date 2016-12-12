using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class PaymentController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();
        private JSONDataController jsonDataController = new JSONDataController();

        public PartialViewResult Payment(string bookType, string id)
        {
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return PartialView("_Error");
            }

            ViewBag.rootUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            ViewBag.parentUrl = CurrentPage.Parent.Url + "?id=" + id;

            if (bookType.Equals("School"))
            {
                return PartialView("_Error");
            }
            else if (bookType.Equals("Adult"))
            {
                AdultModel adult = contentController.GetAdultModelById(Convert.ToInt32(id));
                if (adult == null)
                {
                    return PartialView("_Error");
                }
                
                return PartialView(CONSTVALUE.PARTIAL_VIEW_ADULT_FOLDER + "_AdultPayment.cshtml", adult);
            }
            else if (bookType.Equals("University"))
            {
                return PartialView("_Error");
            }
            return PartialView("_Error");
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostPayment_Adult(AdultModel adult)
        {
            return CurrentUmbracoPage();
        }

        public string GetFingerprint(string input)
        {
            int index = input.IndexOf('|');
            string ESP_PASSWORD = System.Configuration.ConfigurationManager.AppSettings["ESP_PASSWORD"].ToString();
            string temp = input.Insert(++index, ESP_PASSWORD);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(temp);
            // Initialize the engine
            SHA1 engine = new SHA1CryptoServiceProvider();
            // Compute the hash
            byte[] hash = engine.ComputeHash(buffer);

            return System.Text.Encoding.UTF8.GetString(hash, 0, hash.Length);
        }
	}
}