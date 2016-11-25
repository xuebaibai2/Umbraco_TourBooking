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
    public class JSONDataController : SurfaceController
    {
        #region 'Get'
        /// <summary>
        /// Retrieve available Event/Tour information throught web services and return JSON data
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public JsonResult GetJsonData_Event(string eventName)
        {
            List<EventCalendar> eventList = new List<EventCalendar>();
            var jsonResult = GetJsonResultAsString(CONSTVALUE.GET_EVENTFROMNAME + eventName);
            IEnumerable<API_TOUR> tourList = GetDeserializedJsonDataList<API_TOUR>(jsonResult); 
            foreach (var tour in tourList)
            {
                eventList.Add(new EventCalendar
                {
                    id = tour.ID,
                    title = tour.NAME,
                    start = tour.AVAILABLEFROM,
                    end = tour.AVAILABLETO
                });
            }
            return Json(eventList);
        }

        /// <summary>
        /// Retrieve AttendeeCost based on Attendee Type and Event/Tour ID, if no value been retrieved than set cost as 0
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="attendeeType"></param>
        /// <returns></returns>
        public float GetJsonData_AttendeeCost(string eventID, string attendeeType)
        {
            var attendeeTypeResult = GetJsonResultAsString(CONSTVALUE.TOUR_API + eventID + CONSTVALUE.GET_ATTENDEETYPE);
            if (attendeeTypeResult == null)
            {
                return 0;
            }
            IEnumerable<API_TOURATTENDEETYPE> attendeeTypeList = GetDeserializedJsonDataList<API_TOURATTENDEETYPE>(attendeeTypeResult);
            return attendeeTypeList.Where(x => x.TYPE.Equals(attendeeType)).Select(x => x.COST).FirstOrDefault();
        }

        public List<API_TOURATTENDEETYPE> GetJsonData_AttendeeType(string eventID)
        {
            var attendeeTypeResult = GetJsonResultAsString(CONSTVALUE.TOUR_API + eventID + CONSTVALUE.GET_ATTENDEETYPE);
            if (attendeeTypeResult == null)
            {
                return null;
            }
            return GetDeserializedJsonDataList<API_TOURATTENDEETYPE>(attendeeTypeResult).ToList();
        }

        public JsonResult GetSchoolNameList()
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

        /// <summary>
        /// Get all available event name as a SelectListItem List
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetEventNameList()
        {
            List<SelectListItem> schoolProgramList = new List<SelectListItem>();
            var jsonResult = GetJsonResultAsString(CONSTVALUE.GET_ALLEVENTNAME);
            List<string> nameList = GetDeserializedJsonDataList<string>(jsonResult).ToList();
            foreach (var name in nameList)
            {
                schoolProgramList.Add(new SelectListItem()
                {
                    Text = name,
                    Value = name
                });
            }
            return schoolProgramList;
        }

        /// <summary>
        /// Get all YEARGROUP record from thankQ DB LOOKUPVALUE table
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetYearGroupList()
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
        public List<SelectListItem> GetSubjectAreaList()
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
        public string PostNewContact<T>(Object obj, string contactType, string indivisualType = INDIVISUALTYPE.GROUPCOORDINATOR)
        {
            API_CONTACT contact = new API_CONTACT();
            if (typeof(T).Equals(new SchoolModel().GetType()))
            {
                SchoolModel model = (SchoolModel)obj;
                contact.CONTACTTYPE = contactType;
                if (contactType.Equals(CONTACTTYPE.ORGANISATION))
                {
                    MapSchoolContact(contact, model);
                }
                else if (contactType.Equals(CONTACTTYPE.INDIVIDUAL))
                {
                    if (indivisualType.Equals(INDIVISUALTYPE.GROUPCOORDINATOR))
                    {
                        MapSchoolCoordinatorContact(contact,model);
                    }else if (indivisualType.Equals(INDIVISUALTYPE.INVOICEE))
                    {
                        MapSchoolInvoiceeContact(contact, model);
                    }
                }
            }
            string data = new JavaScriptSerializer().Serialize(contact);
            return PostAPI(CONSTVALUE.POST_CONTACT, data);
        }

        public string PostNewTourBooking(string tourID, API_TOURBOOKING tourBooking)
        {
            string data = new JavaScriptSerializer().Serialize(tourBooking);
            return PostAPI(CONSTVALUE.TOUR_API + tourID + CONSTVALUE.POST_TOURBOOKING_SUFFIX, data);
        }

        public string PostNewTourBookingAttendeeSummary(API_TOURBOOKINGATTENDEESUMMARY attemdeeSummary)
        {
            string data = new JavaScriptSerializer().Serialize(attemdeeSummary);
            return PostAPI(CONSTVALUE.TOUR_API + attemdeeSummary.TOURID + CONSTVALUE.POST_TOURBOOKINGATTENDEESUMMARY_MIDDLE+ attemdeeSummary.TOURBOOKINGID + CONSTVALUE.POST_TOURBOOKINGATTENDEESUMMARY_SUFFIX, data);
        }
        #endregion


        #region 'Private Region'
        //GetJsonResult/// Retrieve Jsondata by eventTitle and conver it to serialized string format
        /// </summary>
        /// <param name="eventTitle"></param>
        /// <returns>String format Json Data</returns>
        private string GetSerializedJsonData(string eventTitle)
        {
            return new JavaScriptSerializer().Serialize(GetJsonData_Event(eventTitle).Data);
        }

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
                catch (Exception ex)
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
            contact.PRIMARYCATEGORY = "Schools"; // For university is University
            contact.KEYNAME = model.SchoolName;
        }

        private void MapSchoolCoordinatorContact(API_CONTACT contact, SchoolModel model)
        {
            contact.PRIMARYCATEGORY = "Individual";
            contact.FIRSTNAME = model.Event.GroupCoordinator.FirstName;
            contact.KEYNAME = model.Event.GroupCoordinator.SureName;
            contact.TITLE = model.Event.GroupCoordinator.Title;
            contact.EMAILADDRESS = model.Event.GroupCoordinator.Email;
            contact.MOBILENUMBER = model.Event.GroupCoordinator.Mobile;
            contact.DAYTELEPHONE = model.Event.GroupCoordinator.DaytimeNumber;
        }

        private void MapSchoolInvoiceeContact(API_CONTACT contact, SchoolModel model)
        {
            contact.PRIMARYCATEGORY = "Individual";
            contact.FIRSTNAME = model.Event.Invoice.FirstName;
            contact.KEYNAME = model.Event.Invoice.SureName;
            contact.TITLE = model.Event.Invoice.Title;
            contact.EMAILADDRESS = model.Event.Invoice.Email;
        }

        #endregion

    }
}