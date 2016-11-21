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
            var jsonResult = string.Empty;
            using (WebClient wc = new WebClient())
            {
                List<EventCalendar> result = new List<EventCalendar>();
                try
                {
                    string credentials = Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(CONSTVALUE.API_USERNAME + ":" + CONSTVALUE.API_PASSWORD));
                    wc.Headers[HttpRequestHeader.Authorization] = string.Format(
                        "Basic {0}", credentials);

                    jsonResult = wc.DownloadString(CONSTVALUE.GET_EVENTFROMNAME + eventName);
                }
                catch (Exception ex)
                {
                    return null;
                }
                IEnumerable<REventCalendar> temp = JsonConvert.DeserializeObject<IEnumerable<REventCalendar>>(jsonResult);
                foreach (var e in temp)
                {
                    result.Add(new EventCalendar
                    {
                        id = e.ID,
                        title = e.NAME,
                        start = e.AVAILABLEFROM,
                        end = e.AVAILABLETO,
                        studentPrice = e.ATTENDEECOST
                    });
                }
                return Json(result);
            }
        }
        public List<SelectListItem> GetJsonData_EventName()
        {
            var jsonResult = string.Empty;
            List<SelectListItem> schoolProgramList = new List<SelectListItem>();
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string credentials = Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(CONSTVALUE.API_USERNAME + ":" + CONSTVALUE.API_PASSWORD));
                    wc.Headers[HttpRequestHeader.Authorization] = string.Format(
                        "Basic {0}", credentials);
                    jsonResult = wc.DownloadString(CONSTVALUE.Get_AllEventName);
                }
                catch (Exception ex)
                {
                    return null;
                }
                List<string> tempList = new JavaScriptSerializer().Deserialize<List<string>>(jsonResult);
                foreach (var item in tempList)
                {
                    schoolProgramList.Add(new SelectListItem() { Text = item, Value = item });
                }
                return schoolProgramList;
            }
        }

        #region 'Private Region'
        /// <summary>
        /// Retrieve Jsondata by eventTitle and conver it to serialized string format
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

        #endregion

    }
}