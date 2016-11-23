using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models.API
{
    public class API_TOURATTENDEETYPE
    {
        public string ID { get; set; }
        public string TOURID { get; set; }
        public string REFERENCE { get; set; }
        public string TYPE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string AVAILABLEFROM { get; set; }
        public string AVAILABLETO { get; set; }
        public float COST { get; set; }
    }
}