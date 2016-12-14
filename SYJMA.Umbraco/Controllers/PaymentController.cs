using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                AdultModel adult = contentController.GetModelById_Adult(Convert.ToInt32(id));
                if (adult == null)
                {
                    return PartialView("_Error");
                }
                //Save Group Coordinator on ThankQ DB
                jsonDataController.CreateNewContactOnThankQ<AdultModel>(adult);
                //Create new Tour Booking Record on ThankQ DB
                adult.TourBookingID = jsonDataController.CreateNewTourBookingOnThankQ<AdultModel>(adult);
                //Create new Attendee Summary on ThankQ DB
                jsonDataController.CreateNewTourBookingAttendeeSummaryOnThankQ<AdultModel>(adult);
                //Set Payment for adult
                SetPayment(adult);
                //Save booking record on Umbraco CMS
                contentController.SetPostAdditionalBooking_Adult(adult);
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

        private void SetPayment(AdultModel adult)
        {
            adult.Payment = System.Configuration.ConfigurationManager.GetSection("paymentForm/paymentFormTest") as Payment;
            adult.Payment.EPS_TXNTYPE = "0";
            adult.Payment.EPS_REFERENCEID = "SYJMA"+ " - " + adult.TourBookingID + " - " + adult.GroupName;
            adult.Payment.EPS_AMOUNT = double.Parse(adult.Event.AdditionalInfo.TotalCost, System.Globalization.NumberStyles.Currency).ToString("0.00");
            adult.Payment.EPS_TIMESTAMP = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            string temp_FINGERPRINT = adult.Payment.EPS_MERCHANT + "|" + adult.Payment.ESP_PASSWORD + "|" + adult.Payment.EPS_TXNTYPE
                + "|" + adult.Payment.EPS_REFERENCEID + "|" + adult.Payment.EPS_AMOUNT + "|" + adult.Payment.EPS_TIMESTAMP;

            adult.Payment.EPS_FINGERPRINT = GetFingerprint(temp_FINGERPRINT);
        }

        private string GetFingerprint(string input)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(input);

            using (var sha1 = SHA1.Create())
            {
                byte[] hash = sha1.ComputeHash(buffer);
                return HexStringFromBytes(hash);
            }
        }

        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

	}
}