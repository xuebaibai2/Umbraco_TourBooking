using  System;
using  System.Collections.Generic;
using  System.Linq.Expressions;
using  System.Web;
using  Umbraco.Core.Models;
using  Umbraco.Core.Models.PublishedContent;
using  Umbraco.Web;
using  Umbraco.ModelsBuilder;
using  Umbraco.ModelsBuilder.Umbraco;
[assembly: PureLiveAssembly]
[assembly:ModelsBuilderAssembly(PureLive = true, SourceHash = "2a552d7726d30a6a")]
[assembly:System.Reflection.AssemblyVersion("0.0.0.1")]


// FILE: models.generated.cs

//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.4.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------















namespace Umbraco.Web.PublishedContentModels
{
	/// <summary>Master</summary>
	[PublishedContentModel("master")]
	public partial class Master : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "master";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public Master(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Master, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Page Title: Title of page
		///</summary>
		[ImplementPropertyType("pageTitle")]
		public string PageTitle
		{
			get { return this.GetPropertyValue<string>("pageTitle"); }
		}

		///<summary>
		/// Site Name: Name of site
		///</summary>
		[ImplementPropertyType("siteName")]
		public string SiteName
		{
			get { return this.GetPropertyValue<string>("siteName"); }
		}
	}

	/// <summary>Home</summary>
	[PublishedContentModel("home")]
	public partial class Home : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "home";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public Home(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Home, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Company Logo: Company Logo Upload
		///</summary>
		[ImplementPropertyType("companyLogo")]
		public object CompanyLogo
		{
			get { return this.GetPropertyValue("companyLogo"); }
		}
	}

	/// <summary>Initial Identification</summary>
	[PublishedContentModel("initialIdentification")]
	public partial class InitialIdentification : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "initialIdentification";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public InitialIdentification(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<InitialIdentification, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Book Type: Select Booking Type
		///</summary>
		[ImplementPropertyType("bookType")]
		public object BookType
		{
			get { return this.GetPropertyValue("bookType"); }
		}

		///<summary>
		/// Book Type Description: Descriptioni of Booking type
		///</summary>
		[ImplementPropertyType("bookTypeDescription")]
		public string BookTypeDescription
		{
			get { return this.GetPropertyValue<string>("bookTypeDescription"); }
		}
	}

	/// <summary>Calendar Form</summary>
	[PublishedContentModel("calendarForm")]
	public partial class CalendarForm : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "calendarForm";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public CalendarForm(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<CalendarForm, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Book Type: Type of booking
		///</summary>
		[ImplementPropertyType("bookType")]
		public object BookType
		{
			get { return this.GetPropertyValue("bookType"); }
		}

		///<summary>
		/// Calendar Header: Calendar page header
		///</summary>
		[ImplementPropertyType("calendarHeader")]
		public IHtmlString CalendarHeader
		{
			get { return this.GetPropertyValue<IHtmlString>("calendarHeader"); }
		}
	}

	/// <summary>School</summary>
	[PublishedContentModel("school")]
	public partial class School : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "school";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public School(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<School, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Comments: School booking Comments
		///</summary>
		[ImplementPropertyType("comments")]
		public string Comments
		{
			get { return this.GetPropertyValue<string>("comments"); }
		}

		///<summary>
		/// Name of School: Name of booking school
		///</summary>
		[ImplementPropertyType("nameOfSchool")]
		public string NameOfSchool
		{
			get { return this.GetPropertyValue<string>("nameOfSchool"); }
		}

		///<summary>
		/// Number of Staff: Number of staff for booking
		///</summary>
		[ImplementPropertyType("numberOfStaff")]
		public string NumberOfStaff
		{
			get { return this.GetPropertyValue<string>("numberOfStaff"); }
		}

		///<summary>
		/// Number of Students: Number of students for booking
		///</summary>
		[ImplementPropertyType("numberOfStudents")]
		public string NumberOfStudents
		{
			get { return this.GetPropertyValue<string>("numberOfStudents"); }
		}

		///<summary>
		/// Preferred Date School: Preferred date for school booking
		///</summary>
		[ImplementPropertyType("preferredDateSchool")]
		public DateTime PreferredDateSchool
		{
			get { return this.GetPropertyValue<DateTime>("preferredDateSchool"); }
		}

		///<summary>
		/// Subject Area: School subject area
		///</summary>
		[ImplementPropertyType("subjectArea")]
		public object SubjectArea
		{
			get { return this.GetPropertyValue("subjectArea"); }
		}

		///<summary>
		/// Year: Year level of school booking
		///</summary>
		[ImplementPropertyType("year")]
		public object Year
		{
			get { return this.GetPropertyValue("year"); }
		}
	}

	/// <summary>Adult</summary>
	[PublishedContentModel("adult")]
	public partial class Adult : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "adult";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public Adult(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Adult, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Comments: Adult booking comments
		///</summary>
		[ImplementPropertyType("comments")]
		public string Comments
		{
			get { return this.GetPropertyValue<string>("comments"); }
		}

		///<summary>
		/// Name of Group: Name of booking group
		///</summary>
		[ImplementPropertyType("nameOfGroup")]
		public string NameOfGroup
		{
			get { return this.GetPropertyValue<string>("nameOfGroup"); }
		}

		///<summary>
		/// Number of Adults: Number of adult for booking
		///</summary>
		[ImplementPropertyType("numberOfAdults")]
		public string NumberOfAdults
		{
			get { return this.GetPropertyValue<string>("numberOfAdults"); }
		}

		///<summary>
		/// Preferred Date Adult: Preferred date for adult booking
		///</summary>
		[ImplementPropertyType("preferredDateAdult")]
		public DateTime PreferredDateAdult
		{
			get { return this.GetPropertyValue<DateTime>("preferredDateAdult"); }
		}

		///<summary>
		/// Program: Adult's Program / Tour
		///</summary>
		[ImplementPropertyType("program")]
		public object Program
		{
			get { return this.GetPropertyValue("program"); }
		}
	}

	/// <summary>University</summary>
	[PublishedContentModel("university")]
	public partial class University : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "university";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public University(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<University, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Comments: Comments of university booking
		///</summary>
		[ImplementPropertyType("comments")]
		public string Comments
		{
			get { return this.GetPropertyValue<string>("comments"); }
		}

		///<summary>
		/// Name of Campus: Name of campus for university booking
		///</summary>
		[ImplementPropertyType("nameOfCampus")]
		public string NameOfCampus
		{
			get { return this.GetPropertyValue<string>("nameOfCampus"); }
		}

		///<summary>
		/// Name of University: Name of university for booking
		///</summary>
		[ImplementPropertyType("nameOfUniversity")]
		public string NameOfUniversity
		{
			get { return this.GetPropertyValue<string>("nameOfUniversity"); }
		}

		///<summary>
		/// Number of Staff: University staff number for booking
		///</summary>
		[ImplementPropertyType("numberOfStaff")]
		public string NumberOfStaff
		{
			get { return this.GetPropertyValue<string>("numberOfStaff"); }
		}

		///<summary>
		/// Number of Students: University students number for booking
		///</summary>
		[ImplementPropertyType("numberOfStudents")]
		public string NumberOfStudents
		{
			get { return this.GetPropertyValue<string>("numberOfStudents"); }
		}

		///<summary>
		/// Preferred Date: Preferred date for university booking
		///</summary>
		[ImplementPropertyType("preferredDate")]
		public DateTime PreferredDate
		{
			get { return this.GetPropertyValue<DateTime>("preferredDate"); }
		}

		///<summary>
		/// Program: Program for university
		///</summary>
		[ImplementPropertyType("program")]
		public object Program
		{
			get { return this.GetPropertyValue("program"); }
		}
	}

	/// <summary>Folder</summary>
	[PublishedContentModel("Folder")]
	public partial class Folder : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Folder";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public Folder(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Folder, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Contents:
		///</summary>
		[ImplementPropertyType("contents")]
		public object Contents
		{
			get { return this.GetPropertyValue("contents"); }
		}
	}

	/// <summary>Image</summary>
	[PublishedContentModel("Image")]
	public partial class Image : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Image";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public Image(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Image, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Size
		///</summary>
		[ImplementPropertyType("umbracoBytes")]
		public string UmbracoBytes
		{
			get { return this.GetPropertyValue<string>("umbracoBytes"); }
		}

		///<summary>
		/// Type
		///</summary>
		[ImplementPropertyType("umbracoExtension")]
		public string UmbracoExtension
		{
			get { return this.GetPropertyValue<string>("umbracoExtension"); }
		}

		///<summary>
		/// Upload image
		///</summary>
		[ImplementPropertyType("umbracoFile")]
		public Umbraco.Web.Models.ImageCropDataSet UmbracoFile
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("umbracoFile"); }
		}

		///<summary>
		/// Height
		///</summary>
		[ImplementPropertyType("umbracoHeight")]
		public string UmbracoHeight
		{
			get { return this.GetPropertyValue<string>("umbracoHeight"); }
		}

		///<summary>
		/// Width
		///</summary>
		[ImplementPropertyType("umbracoWidth")]
		public string UmbracoWidth
		{
			get { return this.GetPropertyValue<string>("umbracoWidth"); }
		}
	}

	/// <summary>File</summary>
	[PublishedContentModel("File")]
	public partial class File : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "File";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public File(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<File, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Size
		///</summary>
		[ImplementPropertyType("umbracoBytes")]
		public string UmbracoBytes
		{
			get { return this.GetPropertyValue<string>("umbracoBytes"); }
		}

		///<summary>
		/// Type
		///</summary>
		[ImplementPropertyType("umbracoExtension")]
		public string UmbracoExtension
		{
			get { return this.GetPropertyValue<string>("umbracoExtension"); }
		}

		///<summary>
		/// Upload file
		///</summary>
		[ImplementPropertyType("umbracoFile")]
		public object UmbracoFile
		{
			get { return this.GetPropertyValue("umbracoFile"); }
		}
	}

	/// <summary>Member</summary>
	[PublishedContentModel("Member")]
	public partial class Member : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Member";
		public new const PublishedItemType ModelItemType = PublishedItemType.Member;
#pragma warning restore 0109

		public Member(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Member, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Is Approved
		///</summary>
		[ImplementPropertyType("umbracoMemberApproved")]
		public bool UmbracoMemberApproved
		{
			get { return this.GetPropertyValue<bool>("umbracoMemberApproved"); }
		}

		///<summary>
		/// Comments
		///</summary>
		[ImplementPropertyType("umbracoMemberComments")]
		public string UmbracoMemberComments
		{
			get { return this.GetPropertyValue<string>("umbracoMemberComments"); }
		}

		///<summary>
		/// Failed Password Attempts
		///</summary>
		[ImplementPropertyType("umbracoMemberFailedPasswordAttempts")]
		public string UmbracoMemberFailedPasswordAttempts
		{
			get { return this.GetPropertyValue<string>("umbracoMemberFailedPasswordAttempts"); }
		}

		///<summary>
		/// Last Lockout Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastLockoutDate")]
		public string UmbracoMemberLastLockoutDate
		{
			get { return this.GetPropertyValue<string>("umbracoMemberLastLockoutDate"); }
		}

		///<summary>
		/// Last Login Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastLogin")]
		public string UmbracoMemberLastLogin
		{
			get { return this.GetPropertyValue<string>("umbracoMemberLastLogin"); }
		}

		///<summary>
		/// Last Password Change Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastPasswordChangeDate")]
		public string UmbracoMemberLastPasswordChangeDate
		{
			get { return this.GetPropertyValue<string>("umbracoMemberLastPasswordChangeDate"); }
		}

		///<summary>
		/// Is Locked Out
		///</summary>
		[ImplementPropertyType("umbracoMemberLockedOut")]
		public bool UmbracoMemberLockedOut
		{
			get { return this.GetPropertyValue<bool>("umbracoMemberLockedOut"); }
		}

		///<summary>
		/// Password Answer
		///</summary>
		[ImplementPropertyType("umbracoMemberPasswordRetrievalAnswer")]
		public string UmbracoMemberPasswordRetrievalAnswer
		{
			get { return this.GetPropertyValue<string>("umbracoMemberPasswordRetrievalAnswer"); }
		}

		///<summary>
		/// Password Question
		///</summary>
		[ImplementPropertyType("umbracoMemberPasswordRetrievalQuestion")]
		public string UmbracoMemberPasswordRetrievalQuestion
		{
			get { return this.GetPropertyValue<string>("umbracoMemberPasswordRetrievalQuestion"); }
		}
	}

}



// EOF
