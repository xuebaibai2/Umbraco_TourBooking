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
using SYJMA.Umbraco.Utility;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class JSONDataController : SurfaceController
    {
        /// <summary>
        /// Retrieve data throught web services and return JSON data
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
                    //studentPrice = tour.ATTENDEECOST // retrive data from API_TOURATTENDEETYPE model
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
            var attendeeTypeResult = GetJsonResultAsString(CONSTVALUE.API_HOST + eventID + CONSTVALUE.GET_ATTENDEETYPE);
            if (attendeeTypeResult == null)
            {
                return 0;
            }
            IEnumerable<API_TOURATTENDEETYPE> attendeeTypeList = GetDeserializedJsonDataList<API_TOURATTENDEETYPE>(attendeeTypeResult);
            return attendeeTypeList.Where(x => x.TYPE.Equals(attendeeType)).Select(x => x.COST).FirstOrDefault();
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

        #endregion

    }
}