using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models.ErrorModel
{
    public class PageNotFound : IErrorPage
    {
        public PageNotFound()
        {
            this.ErrorCode = "404";
        }
    }
}