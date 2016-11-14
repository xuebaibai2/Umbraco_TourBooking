using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SYJMA.Umbraco.Models;

namespace SYJMA.Umbraco.Utility
{
    public class JSON
    {
        public IEnumerable<EventCalendar> GetJsonData()
        {
            var jsonResult = string.Empty;
            using (WebClient wc = new WebClient())
            {
                jsonResult = wc.DownloadString("http://demo2054938.mockable.io/1");
                var result = JsonConvert.DeserializeObject<IEnumerable<EventCalendar>>(jsonResult);
                return result;
            }
        }

        public string GetJsonDataString()
        {
            var jsonResult = string.Empty;
            using (WebClient wc = new WebClient())
            {
                return jsonResult = wc.DownloadString("http://demo2054938.mockable.io/1");
            }
        }
    }
}