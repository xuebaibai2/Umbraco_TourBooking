using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYJMA.Umbraco.Models;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace SYJMA.Umbraco.Controllers
{
    public class InitialController : SurfaceController
    {
        private DataTypeController DataType = new DataTypeController();

        /// <summary>
        /// Render partial view for Initial Identification Page
        /// </summary>
        /// <param name="bookType">The type of booking School / Adult / University</param>
        /// <returns>Partial view with Model</returns>
        public PartialViewResult InitialIdentification(string bookType)
        {
            if (bookType.Equals("School"))
            {
                SchoolModel school = new SchoolModel();
                List<SelectListItem> subjectList = new List<SelectListItem>();
                List<SelectListItem> yearList = new List<SelectListItem>();
                //var dropdownSubjectList = DataType.SchoolSubjectDropdownList().PreValuesAsDictionary.Values;
                //foreach (var item in dropdownSubjectList)
                //{
                //    subjectList.Add(new SelectListItem { Text = item.Value, Value = item.Value });
                //}

                //var dropdownYearList = DataType.SchoolYearDropdownList().PreValuesAsDictionary.Values;
                //foreach (var item in dropdownYearList)
                //{
                //    yearList.Add(new SelectListItem { Text = item.Value, Value = item.Value });
                //}

                school.SubjectList = GetDropdownListByListType(DataType.GetSchoolSubjectDropdownList_Values());
                school.YearList = GetDropdownListByListType(DataType.GetSchoolYearDropdownList_Values());
                return PartialView("_SchoolVisit", school);
            }
            //else if (bookType.Equals("Adult"))
            //{
            //    AdultModel adult = new AdultModel();
            //    List<SelectListItem> programList = new List<SelectListItem>();
            //    var dropdownProgramList 
            //}

            // Return page not found
            return null;
        }

        private List<SelectListItem> GetDropdownListByListType(ICollection<PreValue> dropdownList)
        {
            List<SelectListItem> tempList = new List<SelectListItem>();
            foreach (var item in dropdownList)
            {
                tempList.Add(new SelectListItem { Text = item.Value, Value = item.Value});
            }
            return tempList;
        }

        private const int SCHOOLVISIT_CONTENTID = 1081;

        public ActionResult PostInitialPage_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.CreateContent(school.SchoolName + " - " + school.SubjectArea, CurrentPage.Id, "School");

            int selectedYearId = DataType.GetSchoolYearDropdownList_PreValuesAsDictionary()
                .Where(m => m.Value.Value.Equals(school.Year))
                .Select(m => m.Value.Id).First();

            int selectedSubjectId = DataType.GetSchoolSubjectDropdownList_PreValuesAsDictionary()
                .Where(m => m.Value.Value.Equals(school.SubjectArea))
                .Select(m => m.Value.Id).First();

            schoolRecord.SetValue("nameOfSchool",school.SchoolName);
            schoolRecord.SetValue("year", selectedYearId);
            schoolRecord.SetValue("preferredDateSchool", Convert.ToDateTime(school.PreferredDate, new System.Globalization.CultureInfo("en-AU", true)));
            schoolRecord.SetValue("subjectArea", selectedSubjectId);
            schoolRecord.SetValue("numberOfStudents",school.StudentsNumber);
            schoolRecord.SetValue("numberOfStaff",school.StaffNumber);
            schoolRecord.SetValue("comments",school.Comments);
            Services.ContentService.SaveAndPublishWithStatus(schoolRecord);
            return RedirectToCurrentUmbracoPage();
        }
	}
}