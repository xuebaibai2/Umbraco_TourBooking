﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using SYJMA.Umbraco.Models;
using SYJMA.Umbraco.Utility;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class DataTypeController : SurfaceController
    {
        private const int ADULT_PROGRAM_DROPDOWNLIST_KEY = 1060;
        private const int UNI_PROGRAM_DROPDOWNLIST_KEY = 1063;

        /// <summary>
        /// Get User Defined Datatype SchoolSubject Dropdown List values
        /// </summary>
        /// <returns>List of school subject select items</returns>
        public List<SelectListItem> GetSchoolSubjectDropdownList()
        {
            var schoolSubject_Prevalues = ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(GetDateTypeIDByName(CONSTVALUE.SCHOOL_SUBJECT_DROPDOWNLIST_NAME))
                .PreValuesAsDictionary.Values;
            return GetDropdownListByListType(schoolSubject_Prevalues);
        }

        /// <summary>
        /// Get user defiend datatype id by datatype name
        /// </summary>
        /// <param name="datatypeName"></param>
        /// <returns>The Id of the user defined umbraco datatype</returns>
        private int GetDateTypeIDByName(string datatypeName)
        {
            return ApplicationContext.Current.Services.DataTypeService.GetAllDataTypeDefinitions()
                .First(x => datatypeName.InvariantEquals(x.Name)).Id;
        }



        /// <summary>
        /// Get User Defined Datatype SchoolYear Dropdown List values
        /// </summary>
        /// <returns>List of year level select items</returns>
        public List<SelectListItem> GetSchoolYearDropdownList()
        {
            var schoolYear_Prevalues = ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(GetDateTypeIDByName(CONSTVALUE.SCHOOL_YEAR_DROPDOWNLIST_NAME))
                .PreValuesAsDictionary.Values;
            return GetDropdownListByListType(schoolYear_Prevalues);
        }

        /// <summary>
        /// Get User Defined Datatype Adult Program Dropdown List values
        /// </summary>
        /// <returns>List of adult program select items</returns>
        public List<SelectListItem> GetAdultProgramDropdownList()
        {
            var adultProgram_Prevalues = ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(ADULT_PROGRAM_DROPDOWNLIST_KEY).PreValuesAsDictionary.Values;
            return GetDropdownListByListType(adultProgram_Prevalues);
        }

        /// <summary>
        /// Get User Defined Datatype University Program Dropdown List values
        /// </summary>
        /// <returns>List of university program select items</returns>
        public List<SelectListItem> GetUniProgramDropdownList()
        {
            var uniProgram_Prevalues = ApplicationContext.Services.DataTypeService.
                GetPreValuesCollectionByDataTypeId(UNI_PROGRAM_DROPDOWNLIST_KEY).PreValuesAsDictionary.Values;
            return GetDropdownListByListType(uniProgram_Prevalues);
        }

        /// <summary>
        /// Get the selected school subject item id based on selected item name
        /// </summary>
        /// <param name="school"></param>
        /// <returns>subject id</returns>
        public int GetSchoolSubjectDropdownList_SelectedID(SchoolModel school)
        {
            return ApplicationContext.Services.DataTypeService
                .GetPreValuesCollectionByDataTypeId(GetDateTypeIDByName(CONSTVALUE.SCHOOL_SUBJECT_DROPDOWNLIST_NAME))
                .PreValuesAsDictionary
                .Where(m => m.Value.Value.Equals(school.SubjectArea))
                .Select(m => m.Value.Id)
                .First();
        }

        /// <summary>
        /// Get selected dropdown list value based on its id and dropdown list id
        /// </summary>
        /// <param name="listSelectedId"></param>
        /// <param name="dropdownListName"></param>
        /// <returns>The value of user selected from dropdown list</returns>
        public string GetDropdownListValue(int listSelectedId, string dropdownListName)
        {
            return ApplicationContext.Services.DataTypeService
                .GetPreValuesCollectionByDataTypeId(GetDateTypeIDByName(dropdownListName))
                .PreValuesAsDictionary
                .Where(m => m.Value.Id == listSelectedId)
                .Select(m => m.Value.Value)
                .First();
        }

        /// <summary>
        /// Get the selected school year item id based on selected item name
        /// </summary>
        /// <param name="school"></param>
        /// <returns>year id</returns>
        public int GetSchoolYearDropdownList_SelectedID(SchoolModel school)
        {
            return ApplicationContext.Services.DataTypeService
                .GetPreValuesCollectionByDataTypeId(GetDateTypeIDByName(CONSTVALUE.SCHOOL_YEAR_DROPDOWNLIST_NAME))
                .PreValuesAsDictionary
                .Where(m => m.Value.Value.Equals(school.Year))
                .Select(m => m.Value.Id)
                .First();
        }

        /// <summary>
        /// Get the selected school year item id based on selected item name
        /// </summary>
        /// <param name="adult"></param>
        /// <returns>program id</returns>
        public int GetAdultProgramDropdownList_SelectedID(AdultModel adult)
        {
            return ApplicationContext.Services.DataTypeService
                .GetPreValuesCollectionByDataTypeId(ADULT_PROGRAM_DROPDOWNLIST_KEY)
                .PreValuesAsDictionary
                .Where(m => m.Value.Value.Equals(adult.Program))
                .Select(m => m.Value.Id)
                .First();
        }

        /// <summary>
        /// Get the selected university program item id based on selected item name
        /// </summary>
        /// <param name="uni"></param>
        /// <returns>program id</returns>
        public int GetUniProgramDropdownList_SelectedID(UniversityModel uni)
        {
            return ApplicationContext.Services.DataTypeService
                .GetPreValuesCollectionByDataTypeId(UNI_PROGRAM_DROPDOWNLIST_KEY)
                .PreValuesAsDictionary
                .Where(m => m.Value.Value.Equals(uni.Program))
                .Select(m => m.Value.Id)
                .First();
        }

        /// <summary>
        /// Get a List of select items based on the required user defined dropdown list on umbraco
        /// </summary>
        /// <param name="dropdownListValues">Collection of user defined dropdown list's item</param>
        /// <returns></returns>
        private List<SelectListItem> GetDropdownListByListType(ICollection<PreValue> dropdownListValues)
        {
            List<SelectListItem> tempList = new List<SelectListItem>();
            foreach (var item in dropdownListValues)
            {
                tempList.Add(new SelectListItem { Text = item.Value, Value = item.Value });
            }
            return tempList;
        }
    }
}