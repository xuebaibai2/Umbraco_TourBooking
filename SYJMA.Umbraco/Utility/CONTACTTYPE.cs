using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Utility
{
    public struct CONTACTTYPE
    {
        public static string INDIVIDUAL = "Individual";
        public static string ORGANISATION = "Organisation";
    }
    public struct INDIVISUALTYPE
    {
        public const string GROUPCOORDINATOR = "GroupCoordinator";
        public const string INVOICEE = "Invoicee";
    }
    public struct TOURCATEGORY
    {
        public const string SCHOOL = "School";
        public const string ADULT = "Adult";
        public const string UNIVERSITY = "University";
    }
}