using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Models.API;
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public partial class JSONDataController : SurfaceController
    {
        #region 'Get'
        /// <summary>
        /// Retrieve available Event/Tour information throught web services and return JSON data
        /// Called from calendar.control.js, _AdultCalendar.cshtml file
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public JsonResult GetJsonData_Event(string eventName, string category)
        {
            List<EventCalendar> eventList = new List<EventCalendar>();
            var jsonResult = GetJsonResultAsString(string.Format(CONSTVALUE.GET_EVENTFROMNAME,eventName,category));
            IEnumerable<API_TOUR> tourList = GetDeserializedJsonDataList<API_TOUR>(jsonResult); 
            foreach (var tour in tourList)
            {
                eventList.Add(new EventCalendar
                {
                    id = tour.ID,
                    title = tour.NAME,
                    start = tour.AVAILABLEFROM,
                    end = tour.AVAILABLETO,
                    IsInvoiceOnly = tour.INVOICEONLY
                });
            }
            return Json(eventList);
        }

        public List<API_TOURATTENDEETYPE> GetJsonData_AttendeeType(string eventID)
        {
            var attendeeTypeResult = GetJsonResultAsString(string.Format(CONSTVALUE.GET_ATTENDEETYPE, eventID));
            if (attendeeTypeResult == null)
            {
                return null;
            }
            return GetDeserializedJsonDataList<API_TOURATTENDEETYPE>(attendeeTypeResult).ToList();
        }

        /// <summary>
        /// Used to retrieve school name list. This method is been called from getnamelist.js file under Scripts folder
        /// Called from getnamelist.js file
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonData_SchoolNameList()
        {
            List<string> schooResultlList = new List<string>();
            var jsonResult = GetJsonResultAsString(CONSTVALUE.GET_SCHOOLCONTACTS);
            IEnumerable<API_CONTACT> schoolContactList = GetDeserializedJsonDataList<API_CONTACT>(jsonResult);
            foreach (var school in schoolContactList)
            {
                schooResultlList.Add(school.KEYNAME);
            }
            schooResultlList.Sort();
            return Json(schooResultlList);
        }

        public JsonResult GetJsonData_UniNameList()
        {
            List<string> uniResultlList = new List<string>();
            var jsonResult = GetJsonResultAsString(CONSTVALUE.GET_UNICONTACTS);
            IEnumerable<API_CONTACT> uniContactList = GetDeserializedJsonDataList<API_CONTACT>(jsonResult);
            foreach (var uni in uniContactList)
            {
                uniResultlList.Add(uni.KEYNAME);
            }
            uniResultlList.Sort();
            return Json(uniResultlList);
        }

        /// <summary>
        /// Get all available event name as a SelectListItem List
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetJsonData_EventNameList(string category)
        {
            List<SelectListItem> tourProgramList = new List<SelectListItem>();
            var jsonResult = GetJsonResultAsString(string.Format(CONSTVALUE.GET_EVENTNAMELIST,category));
            List<string> nameList = GetDeserializedJsonDataList<string>(jsonResult).ToList();
            foreach (var name in nameList)
            {
                tourProgramList.Add(new SelectListItem()
                {
                    Text = name,
                    Value = name
                });
            }
            return tourProgramList;
        }

        /// <summary>
        /// Get all YEARGROUP record from thankQ DB LOOKUPVALUE table
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetJsonData_YearGroupList()
        {
            List<SelectListItem> yearGroupList = new List<SelectListItem>();
            var jsonResult = GetJsonResultAsString(CONSTVALUE.GET_YEARGROUP);
            List<string> yeargroupStringList = GetDeserializedJsonDataList<string>(jsonResult).ToList();
            foreach (var year in yeargroupStringList)
            {
                yearGroupList.Add(new SelectListItem()
                {
                    Text = year,
                    Value = year
                });
            }
            return yearGroupList;
        }

        /// <summary>
        /// Get all SUBJECTAREA record from thankQ DB LOOKUPVALUE table
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetJsonData_SubjectAreaList()
        {
            List<SelectListItem> subjectAreaList = new List<SelectListItem>();
            var jsonResult = GetJsonResultAsString(CONSTVALUE.GET_SUBJECTAREA);
            List<string> subjectAreaStringList = GetDeserializedJsonDataList<string>(jsonResult).ToList();
            foreach (var subject in subjectAreaStringList)
            {
                subjectAreaList.Add(new SelectListItem()
                {
                    Text = subject,
                    Value = subject 
                });
            }
            return subjectAreaList;
        }

        

        #endregion

        #region 'POST'
        public string PostJsonData_NewContact<T>(Object obj, string contactType, string indivisualType = INDIVISUALTYPE.GROUPCOORDINATOR)
        {
            API_CONTACT contact = new API_CONTACT();
            contact.CONTACTTYPE = contactType;

            if (typeof(T).Equals(new SchoolModel().GetType()))
            {
                SchoolModel model = (SchoolModel)obj;
                if (contactType.Equals(CONTACTTYPE.ORGANISATION))
                {
                    MapSchoolContact(contact, model);
                }
                else if (contactType.Equals(CONTACTTYPE.INDIVIDUAL))
                {
                    if (indivisualType.Equals(INDIVISUALTYPE.GROUPCOORDINATOR))
                    {
                        MapCoordinatorContact_School(contact,model);
                    }else if (indivisualType.Equals(INDIVISUALTYPE.INVOICEE))
                    {
                        MapInvoiceeContact_School(contact, model);
                    }
                }
            }
            else if (typeof(T).Equals(new AdultModel().GetType()))
            {
                AdultModel adult = (AdultModel)obj;
                if (contactType.Equals(CONTACTTYPE.ORGANISATION))
                {

                }
                else if (contactType.Equals(CONTACTTYPE.INDIVIDUAL))
                {
                    if (indivisualType.Equals(INDIVISUALTYPE.GROUPCOORDINATOR))
                    {
                        MapCoordinatorContact_Adult(contact, adult);
                    }
                    else if (indivisualType.Equals(INDIVISUALTYPE.INVOICEE))
                    {
                        MapInvoiceeContact_Adult(contact, adult);
                    }
                }
            }
            else if (typeof(T).Equals(new UniversityModel().GetType()))
            {
                UniversityModel model = (UniversityModel)obj;
                if (contactType.Equals(CONTACTTYPE.ORGANISATION))
                {
                    MapUniversityContact(contact, model);
                }
                else if (contactType.Equals(CONTACTTYPE.INDIVIDUAL))
                {
                    if (indivisualType.Equals(INDIVISUALTYPE.GROUPCOORDINATOR))
                    {
                        MapCoordinatorContact_University(contact, model);
                    }
                    else if (indivisualType.Equals(INDIVISUALTYPE.INVOICEE))
                    {
                        MapInvoiceeContact_University(contact, model);
                    }
                }
            }
            string data = new JavaScriptSerializer().Serialize(contact);
            return PostAPI(CONSTVALUE.POST_CONTACT, data);
        }

        public string PostJsonData_NewTourBooking(string tourID, API_TOURBOOKING tourBooking)
        {
            string data = new JavaScriptSerializer().Serialize(tourBooking);
            return PostAPI(string.Format(CONSTVALUE.POST_TOURBOOKING, tourID), data);
        }

        public string PostJsonData_NewTourBookingAttendeeSummary(API_TOURBOOKINGATTENDEESUMMARY attendeeSummary)
        {
            string data = new JavaScriptSerializer().Serialize(attendeeSummary);
            return PostAPI(string.Format(CONSTVALUE.POST_TOURBOOKINGATTENDEESUMMARY, attendeeSummary.TOURID, attendeeSummary.TOURBOOKINGID), data);
        }
        #endregion


        #region 'Private Region'
        //GetJsonResult/// Retrieve Jsondata by eventTitle and conver it to serialized string format
        /// </summary>
        /// <param name="eventTitle"></param>
        /// <returns>String format Json Data</returns>
        //private string GetSerializedJsonData(string eventTitle)
        //{
        //    return new JavaScriptSerializer().Serialize(GetJsonData_Event(eventTitle).Data);
        //}

        /// <summary>
        /// Convert Serialized Json Data to a List of Object
        /// </summary>
        /// <typeparam name="T">Type of converted list object</typeparam>
        /// <param name="serializedData"></param>
        /// <returns>IEnumerable List of converted Object of Generic type</returns>
        private IEnumerable<T> GetDeserializedJsonDataList<T>(string serializedData)
        {
            return new JavaScriptSerializer().Deserialize<List<T>>(serializedData);
        }

        /// <summary>
        /// Call rest API to retrieve JSON data in string format
        /// </summary>
        /// <param name="apiURL"></param>
        /// <returns></returns>
        private string GetJsonResultAsString(string apiURL)
        {
            var jsonResult = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers[HttpRequestHeader.Authorization] = GetClientCredential();
                    jsonResult = wc.DownloadString(apiURL);
                }
                catch (Exception)
                {
                    return null;
                }
                return jsonResult;
            }
        }

        private string PostAPI(string apiURL, string data)
        {
            string serialNumber = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers[HttpRequestHeader.Authorization] = GetClientCredential();
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    serialNumber = wc.UploadString(apiURL, data);
                }
                catch (Exception ex)
                {
                    return null;
                    throw ex;
                }
                return serialNumber;
            }
        }

        /// <summary>
        /// Get Rest API call Basic Auth credential
        /// </summary>
        /// <returns></returns>
        private string GetClientCredential()
        {
            string credentials = Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(CONSTVALUE.API_USERNAME + ":" + CONSTVALUE.API_PASSWORD));
            return string.Format("Basic {0}", credentials);
        }

        private void MapSchoolContact(API_CONTACT contact, SchoolModel model)
        {
            contact.PRIMARYCATEGORY = TOURCATEGORY.SCHOOL + "s"; 
            contact.KEYNAME = model.SchoolName;
        }

        private void MapUniversityContact(API_CONTACT contact, UniversityModel model)
        {
            contact.PRIMARYCATEGORY = TOURCATEGORY.UNIVERSITY; 
            contact.KEYNAME = model.UniName;
        }

        private void MapCoordinatorContact_School(API_CONTACT contact, SchoolModel model)
        {
            contact.PRIMARYCATEGORY = CONTACTTYPE.INDIVIDUAL;
            contact.FIRSTNAME = model.Event.GroupCoordinator.FirstName;
            contact.KEYNAME = model.Event.GroupCoordinator.SureName;
            contact.TITLE = model.Event.GroupCoordinator.Title;
            contact.EMAILADDRESS = model.Event.GroupCoordinator.Email;
            contact.MOBILENUMBER = model.Event.GroupCoordinator.Mobile;
            contact.DAYTELEPHONE = model.Event.GroupCoordinator.DaytimeNumber;
        }

        private void MapCoordinatorContact_Adult(API_CONTACT contact, AdultModel model)
        {
            contact.PRIMARYCATEGORY = CONTACTTYPE.INDIVIDUAL;
            contact.FIRSTNAME = model.Event.GroupCoordinator.FirstName;
            contact.KEYNAME = model.Event.GroupCoordinator.SureName;
            contact.TITLE = model.Event.GroupCoordinator.Title;
            contact.EMAILADDRESS = model.Event.GroupCoordinator.Email;
            contact.MOBILENUMBER = model.Event.GroupCoordinator.Mobile;
            contact.DAYTELEPHONE = model.Event.GroupCoordinator.DaytimeNumber;
            contact.ADDRESSLINE1 = model.Event.GroupCoordinator.Address;
            contact.SUBURB = model.Event.GroupCoordinator.Suburb;
            contact.STATE = model.Event.GroupCoordinator.State;
            contact.POSTCODE = model.Event.GroupCoordinator.Postcode;
        }

        private void MapCoordinatorContact_University(API_CONTACT contact, UniversityModel model)
        {
            contact.PRIMARYCATEGORY = CONTACTTYPE.INDIVIDUAL;
            contact.FIRSTNAME = model.Event.GroupCoordinator.FirstName;
            contact.KEYNAME = model.Event.GroupCoordinator.SureName;
            contact.TITLE = model.Event.GroupCoordinator.Title;
            contact.EMAILADDRESS = model.Event.GroupCoordinator.Email;
            contact.MOBILENUMBER = model.Event.GroupCoordinator.Mobile;
            contact.DAYTELEPHONE = model.Event.GroupCoordinator.DaytimeNumber;
            contact.ADDRESSLINE1 = model.Event.GroupCoordinator.Address;
            contact.SUBURB = model.Event.GroupCoordinator.Suburb;
            contact.STATE = model.Event.GroupCoordinator.State;
            contact.POSTCODE = model.Event.GroupCoordinator.Postcode;
        }

        private void MapInvoiceeContact_School(API_CONTACT contact, SchoolModel model)
        {
            contact.PRIMARYCATEGORY = CONTACTTYPE.INDIVIDUAL;
            contact.FIRSTNAME = model.Event.Invoice.FirstName;
            contact.KEYNAME = model.Event.Invoice.SureName;
            contact.TITLE = model.Event.Invoice.Title;
            contact.EMAILADDRESS = model.Event.Invoice.Email;
        }

        private void MapInvoiceeContact_Adult(API_CONTACT contact, AdultModel model)
        {
            contact.PRIMARYCATEGORY = CONTACTTYPE.INDIVIDUAL;
            contact.FIRSTNAME = model.Event.Invoice.FirstName;
            contact.KEYNAME = model.Event.Invoice.SureName;
            contact.TITLE = model.Event.Invoice.Title;
            contact.EMAILADDRESS = model.Event.Invoice.Email;
        }

        private void MapInvoiceeContact_University(API_CONTACT contact, UniversityModel model)
        {
            contact.PRIMARYCATEGORY = CONTACTTYPE.INDIVIDUAL;
            contact.FIRSTNAME = model.Event.Invoice.FirstName;
            contact.KEYNAME = model.Event.Invoice.SureName;
            contact.TITLE = model.Event.Invoice.Title;
            contact.EMAILADDRESS = model.Event.Invoice.Email;
        }

        #endregion

    }
}