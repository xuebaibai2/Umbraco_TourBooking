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
    public class BookingDetailController : SurfaceController
    {
        private DataTypeController dataTypeController = new DataTypeController();
        private ContentController contentController = new ContentController();

        /// <summary>
        /// Render Partial View based on the bookType and book model id
        /// </summary>
        /// <param name="bookType"></param>
        /// <param name="id"></param>
        /// <returns>Partial view based on the booktype and the model</returns>
        public PartialViewResult BookingDetail(string bookType, string id)
        {
            int result;
            if (!Int32.TryParse(id, out result))
            {
                return PartialView("_Error");
            }

            if (bookType.Equals("School"))
            {
                SchoolModel school = contentController.GetSchoolModelById(Convert.ToInt32(id));
                if (school == null)
                {
                    return PartialView("_Error");
                }
                ViewBag.parentUrl = CurrentPage.Parent.Url + "?id=" + id;
                return PartialView("~/Views/Partials/School/_SchoolBookingDetail.cshtml", school);
            }
            else if (bookType.Equals("Adult"))
            {

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
        public ActionResult PostBooking_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.GetById(school.Id);
            schoolRecord.SetValue("title", school.Event.GroupCoordinator.Title);
            schoolRecord.SetValue("firstName", school.Event.GroupCoordinator.FirstName);
            schoolRecord.SetValue("surename", school.Event.GroupCoordinator.SureName);
            schoolRecord.SetValue("email", school.Event.GroupCoordinator.Email);
            schoolRecord.SetValue("mobile", school.Event.GroupCoordinator.Mobile);
            schoolRecord.SetValue("daytimeNumber", school.Event.GroupCoordinator.DaytimeNumber);

            schoolRecord.SetValue("invoiceTitle", school.Event.GroupCoordinator.Invoice.Title);
            schoolRecord.SetValue("invoiceFirstName", school.Event.GroupCoordinator.Invoice.FirstName);
            schoolRecord.SetValue("invoiceSurename", school.Event.GroupCoordinator.Invoice.SureName);
            schoolRecord.SetValue("invoiceEmail", school.Event.GroupCoordinator.Invoice.Email);
            Services.ContentService.Save(schoolRecord);

            NameValueCollection routeValues = new NameValueCollection();
            routeValues.Add("id", school.Id.ToString());

            return RedirectToUmbracoPage(contentController.GetContentIDFromSelf("SchoolAdditionalDetail", CurrentPage), routeValues);
        }
	}
}