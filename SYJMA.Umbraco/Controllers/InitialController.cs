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
                adult.ListOfProgram = DataType.GetAdultProgramDropdownList();
                return PartialView("_AdultVisit", adult);
            }
            else if (bookType.Equals("University"))
            {
                UniversityModel uni = new UniversityModel();
                uni.ListOfProgram = DataType.GetUniProgramDropdownList();
                return PartialView("_UniversityVisit",uni);
            }
            // Return page not found
            return null;
        }

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

        private DateTime GetDateTime(InitialModel viewModel)
        {
            return Convert.ToDateTime(viewModel.PreferredDate, new System.Globalization.CultureInfo("en-AU", true));
        }
	}
}