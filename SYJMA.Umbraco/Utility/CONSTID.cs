﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Utility
{
    /// <summary>
    /// Constant value used on this project
    /// </summary>
    public static class CONSTVALUE
    {
        
        //public const int SCHOOL_YEAR_DROPDOWNLIST_KEY = 1056;
        //public const int SCHOOL_SUBJECT_DROPDOWNLIST_KEY = 1057;

        /// <summary>
        /// The API Link
        /// </summary>
        //public const string API_HOST = "http://demo2054938.mockable.io/";
        public const string API_HOST = "http://localhost:58015/Tour/";
        public const string GET_EVENTFROMNAME = "http://localhost:58015/Tour?tourname=";
        public const string Get_AllEventName = "http://localhost:58015/Tour/AllNames";

        public const string API_USERNAME = "SYJMA";
        public const string API_PASSWORD = "slowghost30";
        /// <summary>
        /// Custom Datatype Names
        /// </summary>
        public const string SCHOOL_YEAR_DROPDOWNLIST_NAME = "SchoolYearDropdown";
        public const string SCHOOL_SUBJECT_DROPDOWNLIST_NAME = "SchoolSubjectDropdown";

        /// <summary>
        /// Content Names
        /// </summary>
        public const string SCHOOL_VISITS_CONTENT = "School Visits";
    }
}