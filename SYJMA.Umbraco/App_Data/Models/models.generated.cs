//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.4.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

[assembly: PureLiveAssembly]
[assembly:ModelsBuilderAssembly(PureLive = true, SourceHash = "fd5a3bdad36cf0e6")]
[assembly:System.Reflection.AssemblyVersion("0.0.0.2")]

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

		///<summary>
		/// Program Description Link
		///</summary>
		[ImplementPropertyType("programDescriptionLink")]
		public IHtmlString ProgramDescriptionLink
		{
			get { return this.GetPropertyValue<IHtmlString>("programDescriptionLink"); }
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
		/// AdditionalDetails: Additional Details or Requirements
		///</summary>
		[ImplementPropertyType("additionalDetails")]
		public string AdditionalDetails
		{
			get { return this.GetPropertyValue<string>("additionalDetails"); }
		}

		///<summary>
		/// CafeRequirement: Is Cafe Catering Required
		///</summary>
		[ImplementPropertyType("cafeRequirement")]
		public string CafeRequirement
		{
			get { return this.GetPropertyValue<string>("cafeRequirement"); }
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
		/// Content Knowledge
		///</summary>
		[ImplementPropertyType("contentKnowledge")]
		public string ContentKnowledge
		{
			get { return this.GetPropertyValue<string>("contentKnowledge"); }
		}

		///<summary>
		/// Daytime Number: Group Coordinator Daytime Number
		///</summary>
		[ImplementPropertyType("daytimeNumber")]
		public string DaytimeNumber
		{
			get { return this.GetPropertyValue<string>("daytimeNumber"); }
		}

		///<summary>
		/// Email: Group Coordinator Email
		///</summary>
		[ImplementPropertyType("email")]
		public string Email
		{
			get { return this.GetPropertyValue<string>("email"); }
		}

		///<summary>
		/// Event End: Event end time
		///</summary>
		[ImplementPropertyType("eventEnd")]
		public string EventEnd
		{
			get { return this.GetPropertyValue<string>("eventEnd"); }
		}

		///<summary>
		/// Event Id: Tour ID
		///</summary>
		[ImplementPropertyType("eventId")]
		public string EventId
		{
			get { return this.GetPropertyValue<string>("eventId"); }
		}

		///<summary>
		/// Event Price Staff: Staff Booking Price
		///</summary>
		[ImplementPropertyType("eventPriceStaff")]
		public string EventPriceStaff
		{
			get { return this.GetPropertyValue<string>("eventPriceStaff"); }
		}

		///<summary>
		/// Event Price Student: Student Booking Price
		///</summary>
		[ImplementPropertyType("eventPriceStudent")]
		public string EventPriceStudent
		{
			get { return this.GetPropertyValue<string>("eventPriceStudent"); }
		}

		///<summary>
		/// Event Start: Event start time
		///</summary>
		[ImplementPropertyType("eventStart")]
		public string EventStart
		{
			get { return this.GetPropertyValue<string>("eventStart"); }
		}

		///<summary>
		/// Event Title: Booking event title
		///</summary>
		[ImplementPropertyType("eventTitle")]
		public string EventTitle
		{
			get { return this.GetPropertyValue<string>("eventTitle"); }
		}

		///<summary>
		/// First Name: Group Coordinator Firstname
		///</summary>
		[ImplementPropertyType("firstName")]
		public string FirstName
		{
			get { return this.GetPropertyValue<string>("firstName"); }
		}

		///<summary>
		/// GroupCoordinatorSerialNumber: Group Coordinator Serial Number from ThankQ DB Contact Table
		///</summary>
		[ImplementPropertyType("groupCoordinatorSerialNumber")]
		public string GroupCoordinatorSerialNumber
		{
			get { return this.GetPropertyValue<string>("groupCoordinatorSerialNumber"); }
		}

		///<summary>
		/// Invoice Email: Invoice Receiver's Email
		///</summary>
		[ImplementPropertyType("invoiceEmail")]
		public string InvoiceEmail
		{
			get { return this.GetPropertyValue<string>("invoiceEmail"); }
		}

		///<summary>
		/// InvoiceeSerialNumber: Invoicee SerialNumber from ThankQ DB Contact Table
		///</summary>
		[ImplementPropertyType("invoiceeSerialNumber")]
		public string InvoiceeSerialNumber
		{
			get { return this.GetPropertyValue<string>("invoiceeSerialNumber"); }
		}

		///<summary>
		/// Invoice First Name: Invoice Receiver's First Name
		///</summary>
		[ImplementPropertyType("invoiceFirstName")]
		public string InvoiceFirstName
		{
			get { return this.GetPropertyValue<string>("invoiceFirstName"); }
		}

		///<summary>
		/// Invoice Surename: Invoice Receiver's Surename
		///</summary>
		[ImplementPropertyType("invoiceSurename")]
		public string InvoiceSurename
		{
			get { return this.GetPropertyValue<string>("invoiceSurename"); }
		}

		///<summary>
		/// Invoice Title: Invoice Receiver's Title
		///</summary>
		[ImplementPropertyType("invoiceTitle")]
		public string InvoiceTitle
		{
			get { return this.GetPropertyValue<string>("invoiceTitle"); }
		}

		///<summary>
		/// Is Same Contact
		///</summary>
		[ImplementPropertyType("isSameContact")]
		public string IsSameContact
		{
			get { return this.GetPropertyValue<string>("isSameContact"); }
		}

		///<summary>
		/// MainBookingID
		///</summary>
		[ImplementPropertyType("mainBookingID")]
		public string MainBookingID
		{
			get { return this.GetPropertyValue<string>("mainBookingID"); }
		}

		///<summary>
		/// Mobile: Group Coordinator Mobile Number
		///</summary>
		[ImplementPropertyType("mobile")]
		public string Mobile
		{
			get { return this.GetPropertyValue<string>("mobile"); }
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
		/// Per Cost: Cost per Students Attending
		///</summary>
		[ImplementPropertyType("perCost")]
		public string PerCost
		{
			get { return this.GetPropertyValue<string>("perCost"); }
		}

		///<summary>
		/// Preferred Date School: Preferred date for school booking
		///</summary>
		[ImplementPropertyType("preferredDateSchool")]
		public string PreferredDateSchool
		{
			get { return this.GetPropertyValue<string>("preferredDateSchool"); }
		}

		///<summary>
		/// Record Id: Record Id
		///</summary>
		[ImplementPropertyType("recordId")]
		public string RecordId
		{
			get { return this.GetPropertyValue<string>("recordId"); }
		}

		///<summary>
		/// SchoolSerialNumber: Serial Number of School from ThankQ DB
		///</summary>
		[ImplementPropertyType("schoolSerialNumber")]
		public string SchoolSerialNumber
		{
			get { return this.GetPropertyValue<string>("schoolSerialNumber"); }
		}

		///<summary>
		/// Staff Attendee Type ID
		///</summary>
		[ImplementPropertyType("staffAttendeeTypeID")]
		public string StaffAttendeeTypeID
		{
			get { return this.GetPropertyValue<string>("staffAttendeeTypeID"); }
		}

		///<summary>
		/// Student Attendee Type ID
		///</summary>
		[ImplementPropertyType("studentAttendeeTypeID")]
		public string StudentAttendeeTypeID
		{
			get { return this.GetPropertyValue<string>("studentAttendeeTypeID"); }
		}

		///<summary>
		/// Subject Area: School subject area
		///</summary>
		[ImplementPropertyType("subjectArea")]
		public string SubjectArea
		{
			get { return this.GetPropertyValue<string>("subjectArea"); }
		}

		///<summary>
		/// Surename: Group Coordinator Surename
		///</summary>
		[ImplementPropertyType("surename")]
		public string Surename
		{
			get { return this.GetPropertyValue<string>("surename"); }
		}

		///<summary>
		/// Title: Group Coordinator Title
		///</summary>
		[ImplementPropertyType("title")]
		public string Title
		{
			get { return this.GetPropertyValue<string>("title"); }
		}

		///<summary>
		/// Total Cost: Total Cost for student booking
		///</summary>
		[ImplementPropertyType("totalCost")]
		public string TotalCost
		{
			get { return this.GetPropertyValue<string>("totalCost"); }
		}

		///<summary>
		/// TourBookingID: Tour Booking ID from ThankQ DB
		///</summary>
		[ImplementPropertyType("tourBookingID")]
		public string TourBookingID
		{
			get { return this.GetPropertyValue<string>("tourBookingID"); }
		}

		///<summary>
		/// Year: Year level of school booking
		///</summary>
		[ImplementPropertyType("year")]
		public string Year
		{
			get { return this.GetPropertyValue<string>("year"); }
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
		/// Attendee Type ID
		///</summary>
		[ImplementPropertyType("attendeeTypeID")]
		public string AttendeeTypeID
		{
			get { return this.GetPropertyValue<string>("attendeeTypeID"); }
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
		/// Daytime Number: Group Coordinator Daytime Number
		///</summary>
		[ImplementPropertyType("daytimeNumber")]
		public string DaytimeNumber
		{
			get { return this.GetPropertyValue<string>("daytimeNumber"); }
		}

		///<summary>
		/// Email: Group Coordinator Email
		///</summary>
		[ImplementPropertyType("email")]
		public string Email
		{
			get { return this.GetPropertyValue<string>("email"); }
		}

		///<summary>
		/// Event End: Event end time
		///</summary>
		[ImplementPropertyType("eventEnd")]
		public string EventEnd
		{
			get { return this.GetPropertyValue<string>("eventEnd"); }
		}

		///<summary>
		/// Event Id: Tour ID
		///</summary>
		[ImplementPropertyType("eventId")]
		public string EventId
		{
			get { return this.GetPropertyValue<string>("eventId"); }
		}

		///<summary>
		/// Event Price: Adult Event Price
		///</summary>
		[ImplementPropertyType("eventPrice")]
		public string EventPrice
		{
			get { return this.GetPropertyValue<string>("eventPrice"); }
		}

		///<summary>
		/// Event Start: Event start time
		///</summary>
		[ImplementPropertyType("eventStart")]
		public string EventStart
		{
			get { return this.GetPropertyValue<string>("eventStart"); }
		}

		///<summary>
		/// Event Title: Booking  event title
		///</summary>
		[ImplementPropertyType("eventTitle")]
		public string EventTitle
		{
			get { return this.GetPropertyValue<string>("eventTitle"); }
		}

		///<summary>
		/// First Name: Group Coordinator Firstname
		///</summary>
		[ImplementPropertyType("firstName")]
		public string FirstName
		{
			get { return this.GetPropertyValue<string>("firstName"); }
		}

		///<summary>
		/// Group Coordinator Serial Number: Group Coordinator Serial Number from ThankQ DB contact Table
		///</summary>
		[ImplementPropertyType("groupCoordinatorSerialNumber")]
		public string GroupCoordinatorSerialNumber
		{
			get { return this.GetPropertyValue<string>("groupCoordinatorSerialNumber"); }
		}

		///<summary>
		/// Invoice Email: Invoice Receiver's Email
		///</summary>
		[ImplementPropertyType("invoiceEmail")]
		public string InvoiceEmail
		{
			get { return this.GetPropertyValue<string>("invoiceEmail"); }
		}

		///<summary>
		/// Invoicee Serial Number: Invoicee Serial Number from ThankQ DB Contact Table
		///</summary>
		[ImplementPropertyType("invoiceeSerialNumber")]
		public string InvoiceeSerialNumber
		{
			get { return this.GetPropertyValue<string>("invoiceeSerialNumber"); }
		}

		///<summary>
		/// Invoice First Name: Invoice Receiver's First Name
		///</summary>
		[ImplementPropertyType("invoiceFirstName")]
		public string InvoiceFirstName
		{
			get { return this.GetPropertyValue<string>("invoiceFirstName"); }
		}

		///<summary>
		/// Invoice Surename: Invoice Receiver's Surename
		///</summary>
		[ImplementPropertyType("invoiceSurename")]
		public string InvoiceSurename
		{
			get { return this.GetPropertyValue<string>("invoiceSurename"); }
		}

		///<summary>
		/// Invoice Title: Invoice Receiver's Title
		///</summary>
		[ImplementPropertyType("invoiceTitle")]
		public string InvoiceTitle
		{
			get { return this.GetPropertyValue<string>("invoiceTitle"); }
		}

		///<summary>
		/// Is Same Contact
		///</summary>
		[ImplementPropertyType("isSameContact")]
		public string IsSameContact
		{
			get { return this.GetPropertyValue<string>("isSameContact"); }
		}

		///<summary>
		/// Mobile: Group Coordinator Mobile
		///</summary>
		[ImplementPropertyType("mobile")]
		public string Mobile
		{
			get { return this.GetPropertyValue<string>("mobile"); }
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
		/// Number of Adults
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
		public string PreferredDateAdult
		{
			get { return this.GetPropertyValue<string>("preferredDateAdult"); }
		}

		///<summary>
		/// Program: Adult's Program / Tour
		///</summary>
		[ImplementPropertyType("program")]
		public string Program
		{
			get { return this.GetPropertyValue<string>("program"); }
		}

		///<summary>
		/// Record Id
		///</summary>
		[ImplementPropertyType("recordId")]
		public string RecordId
		{
			get { return this.GetPropertyValue<string>("recordId"); }
		}

		///<summary>
		/// Surename: Group Coordinator Surename
		///</summary>
		[ImplementPropertyType("surename")]
		public string Surename
		{
			get { return this.GetPropertyValue<string>("surename"); }
		}

		///<summary>
		/// Title: Group Coordinator Title
		///</summary>
		[ImplementPropertyType("title")]
		public string Title
		{
			get { return this.GetPropertyValue<string>("title"); }
		}

		///<summary>
		/// TourBookingID: Tour Booking ID from ThankQ DB
		///</summary>
		[ImplementPropertyType("tourBookingID")]
		public string TourBookingID
		{
			get { return this.GetPropertyValue<string>("tourBookingID"); }
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

		///<summary>
		/// Record Id: Record Id
		///</summary>
		[ImplementPropertyType("recordId")]
		public string RecordId
		{
			get { return this.GetPropertyValue<string>("recordId"); }
		}
	}

	/// <summary>Confirmation panel</summary>
	[PublishedContentModel("confirmationPanel")]
	public partial class ConfirmationPanel : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "confirmationPanel";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public ConfirmationPanel(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ConfirmationPanel, TValue>> selector)
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
		/// Page Header: Page Description
		///</summary>
		[ImplementPropertyType("pageHeader")]
		public string PageHeader
		{
			get { return this.GetPropertyValue<string>("pageHeader"); }
		}
	}

	/// <summary>Booking Detail</summary>
	[PublishedContentModel("bookingDetail")]
	public partial class BookingDetail : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "bookingDetail";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public BookingDetail(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<BookingDetail, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Book Type: Booking Type
		///</summary>
		[ImplementPropertyType("bookType")]
		public object BookType
		{
			get { return this.GetPropertyValue("bookType"); }
		}

		///<summary>
		/// Page Header
		///</summary>
		[ImplementPropertyType("pageHeader")]
		public string PageHeader
		{
			get { return this.GetPropertyValue<string>("pageHeader"); }
		}
	}

	/// <summary>Addition Booking Detail</summary>
	[PublishedContentModel("additionBookingDetail")]
	public partial class AdditionBookingDetail : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "additionBookingDetail";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public AdditionBookingDetail(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<AdditionBookingDetail, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Book Type
		///</summary>
		[ImplementPropertyType("bookType")]
		public object BookType
		{
			get { return this.GetPropertyValue("bookType"); }
		}

		///<summary>
		/// Cafe Menu Link
		///</summary>
		[ImplementPropertyType("cafeMenuLink")]
		public string CafeMenuLink
		{
			get { return this.GetPropertyValue<string>("cafeMenuLink"); }
		}

		///<summary>
		/// Page Header
		///</summary>
		[ImplementPropertyType("pageHeader")]
		public string PageHeader
		{
			get { return this.GetPropertyValue<string>("pageHeader"); }
		}

		///<summary>
		/// Risk Assessment Form Link
		///</summary>
		[ImplementPropertyType("riskAssessmentFormLink")]
		public string RiskAssessmentFormLink
		{
			get { return this.GetPropertyValue<string>("riskAssessmentFormLink"); }
		}

		///<summary>
		/// SJM Officer Contact
		///</summary>
		[ImplementPropertyType("sJMOfficerContact")]
		public string SJmofficerContact
		{
			get { return this.GetPropertyValue<string>("sJMOfficerContact"); }
		}
	}

	/// <summary>Complete Booking</summary>
	[PublishedContentModel("completeBooking")]
	public partial class CompleteBooking : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "completeBooking";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public CompleteBooking(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<CompleteBooking, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Additional Info1: Display message intended for marketing or additional information
		///</summary>
		[ImplementPropertyType("additionalInfo1")]
		public IHtmlString AdditionalInfo1
		{
			get { return this.GetPropertyValue<IHtmlString>("additionalInfo1"); }
		}

		///<summary>
		/// Additional Info 2: Additional message area
		///</summary>
		[ImplementPropertyType("additionalInfo2")]
		public IHtmlString AdditionalInfo2
		{
			get { return this.GetPropertyValue<IHtmlString>("additionalInfo2"); }
		}

		///<summary>
		/// Page Header
		///</summary>
		[ImplementPropertyType("pageHeader")]
		public IHtmlString PageHeader
		{
			get { return this.GetPropertyValue<IHtmlString>("pageHeader"); }
		}
	}

	/// <summary>Initial Identification Subtour</summary>
	[PublishedContentModel("initialIdentificationSubtour")]
	public partial class InitialIdentificationSubtour : Master
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "initialIdentificationSubtour";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public InitialIdentificationSubtour(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<InitialIdentificationSubtour, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Book Type
		///</summary>
		[ImplementPropertyType("bookType")]
		public object BookType
		{
			get { return this.GetPropertyValue("bookType"); }
		}

		///<summary>
		/// Book Type Description
		///</summary>
		[ImplementPropertyType("bookTypeDescription")]
		public string BookTypeDescription
		{
			get { return this.GetPropertyValue<string>("bookTypeDescription"); }
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
