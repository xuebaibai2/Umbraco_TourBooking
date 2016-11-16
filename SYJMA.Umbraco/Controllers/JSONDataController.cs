using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public JsonResult GetJsonData(string eventName)
        {
            var jsonResult = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    jsonResult = wc.DownloadString(CONSTVALUE.API_HOST + eventName);
                }
                catch (Exception)
                {
                    return null;
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<EventCalendar>>(jsonResult);
                return Json(result);
            }
        }

        /// <summary>
        /// Retrieve Jsondata by eventTitle and conver it to serialized string format
        /// </summary>
        /// <param name="eventTitle"></param>
        /// <returns>String format Json Data</returns>
        private string GetSerializedJsonData(string eventTitle)
        {
            return new JavaScriptSerializer().Serialize(GetJsonData(eventTitle).Data);
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
        /// Get All School Program List from API Call
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSchoolProgramList()
        {
            List<SelectListItem> schoolProgramList = new List<SelectListItem>();

            string resultString = GetSerializedJsonData("getAllSchoolProgram");
            List<EventCalendar> wrapper = GetDeserializedJsonDataList<EventCalendar>(resultString).ToList();

            foreach (var item in wrapper)
            {
                schoolProgramList.Add(new SelectListItem() { Text = item.title, Value = item.title });
            }
            return schoolProgramList;
        }
	}
}