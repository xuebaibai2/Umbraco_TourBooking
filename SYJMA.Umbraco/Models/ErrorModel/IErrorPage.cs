using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models.ErrorModel
{
    public class IErrorPage
    {
        public string ErrorCode { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorDescription { get; set; }
    }
}