using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class DataTypeController : SurfaceController
    {
        private const int SCHOOL_SUBJECT_DROPDOWNLIST_KEY = 1057;
        private const int SCHOOL_YEAR_DROPDOWNLIST_KEY = 1056;

        /// <summary>
        /// Get User Defined Datatype SchoolSubject Dropdown List
        /// </summary>
        /// <returns>School Subject Dropdown List in IDictionary format</returns>
        public IDictionary<string, PreValue> GetSchoolSubjectDropdownList_PreValuesAsDictionary()
        {
            return ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(SCHOOL_SUBJECT_DROPDOWNLIST_KEY).PreValuesAsDictionary;
        }

        /// <summary>
        /// Get User Defined Datatype SchoolSubject Dropdown List values
        /// </summary>
        /// <returns>List of subject values</returns>
        public ICollection<PreValue> GetSchoolSubjectDropdownList_Values()
        {
            return ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(SCHOOL_SUBJECT_DROPDOWNLIST_KEY).PreValuesAsDictionary.Values;
        }

        /// <summary>
        /// Get User Defined Datatype SchoolYear Dropdown List
        /// </summary>
        /// <returns>School Year Dropdown List in IDictionary format</returns>
        public IDictionary<string, PreValue> GetSchoolYearDropdownList_PreValuesAsDictionary()
        {
            return ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(SCHOOL_YEAR_DROPDOWNLIST_KEY).PreValuesAsDictionary;
        }

        /// <summary>
        /// Get User Defined Datatype SchoolYear Dropdown List values
        /// </summary>
        /// <returns>List of year level values</returns>
        public ICollection<PreValue> GetSchoolYearDropdownList_Values()
        {
            return ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(SCHOOL_YEAR_DROPDOWNLIST_KEY).PreValuesAsDictionary.Values;
        }

    }
}