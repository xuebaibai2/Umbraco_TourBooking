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
                school.SubjectList = DataType.GetSchoolSubjectDropdownList();
                school.YearList = DataType.GetSchoolYearDropdownList();
                return PartialView("_SchoolVisit", school);
            }
            else if (bookType.Equals("Adult"))
            {
                AdultModel adult = new AdultModel();
                adult.ProgramList = DataType.GetAdultProgramDropdownList();
                return PartialView("_AdultVisit", adult);
            }
            else if (bookType.Equals("University"))
            {
                UniversityModel uni = new UniversityModel();
                uni.ProgramList = DataType.GetUniProgramDropdownList();
                return PartialView("_UniversityVisit",uni);
            }
            // Return page not found
            return null;
        }

        /// <summary>
        /// Receive post data form from School partialview and save into School Visits content as a record
        /// </summary>
        /// <param name="school"></param>
        /// <returns>Redirect to page</returns>
        public ActionResult PostInitialPage_School(SchoolModel school)
        {
            var schoolRecord = Services.ContentService.CreateContent(school.SchoolName + " - " + school.SubjectArea, CurrentPage.Id, "School");

            int selectedYearId = DataType.GetSchoolYearDropdownList_SelectedID(school);
            int selectedSubjectId = DataType.GetSchoolSubjectDropdownList_SelectedID(school);

            schoolRecord.SetValue("nameOfSchool",school.SchoolName);
            schoolRecord.SetValue("year", selectedYearId);
            schoolRecord.SetValue("preferredDateSchool", GetDateTime(school));
            schoolRecord.SetValue("subjectArea", selectedSubjectId);
            schoolRecord.SetValue("numberOfStudents",school.StudentsNumber);
            schoolRecord.SetValue("numberOfStaff",school.StaffNumber);
            schoolRecord.SetValue("comments",school.Comments);
            Services.ContentService.SaveAndPublishWithStatus(schoolRecord);
            return RedirectToCurrentUmbracoPage();
        }

        /// <summary>
        /// Receive post data form from Adult partialview and save into Adult Visits content as a record
        /// </summary>
        /// <param name="adult"></param>
        /// <returns>Redirect to page</returns>
        public ActionResult PostInitialPage_Adult(AdultModel adult)
        {
            var adultRecord = Services.ContentService.CreateContent(adult.GroupName + " - " + adult.Program, CurrentPage.Id, "Adult");

            int selectedProgramId = DataType.GetAdultProgramDropdownList_SelectedID(adult);

            adultRecord.SetValue("nameOfGroup",adult.GroupName);
            adultRecord.SetValue("program",selectedProgramId);
            adultRecord.SetValue("preferredDateAdult", GetDateTime(adult));
            adultRecord.SetValue("numberOfAdults", adult.AdultNumber);
            adultRecord.SetValue("comments", adult.Comments);
            Services.ContentService.SaveAndPublishWithStatus(adultRecord);
            return RedirectToCurrentUmbracoPage();
        }

        /// <summary>
        /// Receive post data form from University partialview and save into University Visits content as a record
        /// </summary>
        /// <param name="uni"></param>
        /// <returns>Redirect to page</returns>
        public ActionResult PostInitialPage_Uni(UniversityModel uni)
        {
            var uniRecord = Services.ContentService.CreateContent(uni.UniName + " - " + uni.Program, CurrentPage.Id, "University");

            int selectedProgramId = DataType.GetUniProgramDropdownList_SelectedID(uni);

            uniRecord.SetValue("nameOfUniversity", uni.UniName);
            uniRecord.SetValue("nameOfCampus", uni.CampusName);
            uniRecord.SetValue("program", selectedProgramId);
            uniRecord.SetValue("preferredDate", GetDateTime(uni));
            uniRecord.SetValue("numberOfStudents", uni.StudentNumber);
            uniRecord.SetValue("numberOfStaff", uni.StaffNumber);
            uniRecord.SetValue("comments", uni.Comments);
            Services.ContentService.SaveAndPublishWithStatus(uniRecord);
            return RedirectToCurrentUmbracoPage();
        }

        /// <summary>
        /// Convert string format of datetime to DateTime datatype
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Preferred booking date with datetime format</returns>
        private DateTime GetDateTime(InitialModel viewModel)
        {
            return Convert.ToDateTime(viewModel.PreferredDate, new System.Globalization.CultureInfo("en-AU", true));
        }
	}
}