
namespace Photo.Business.Utilities.Communication
{
	#region General

	/// <summary>
	/// Enumeration for the types of different attachment to be downloaded
	/// </summary>
	public enum ApplicationType : byte
	{
		/// <summary>
		/// The PDF
		/// </summary>
		Pdf,
		/// <summary>
		/// The XLSX
		/// </summary>
		Xlsx
	}

	#endregion


	#region Freshdesk

	/// <summary>
	/// Indicates the priority of the ticket raised
	/// </summary>
	public enum FreshdeskTicketPriority : byte
	{
		/// <summary>
		/// Low
		/// </summary>
		Low = 1,

		/// <summary>
		/// Medium
		/// </summary>
		Medium = 2,

		/// <summary>
		/// High
		/// </summary>
		High = 3,

		/// <summary>
		/// Urgent
		/// </summary>
		Urgent = 4
	}

	/// <summary>
	/// Indicates the status of the ticket raised
	/// </summary>
	public enum FreshdeskTicketStatus : byte
	{
		/// <summary>
		/// Open
		/// </summary>
		Open = 2,

		/// <summary>
		/// Pending
		/// </summary>
		Pending = 3,

		/// <summary>
		/// Resolved
		/// </summary>
		Resolved = 4,

		/// <summary>
		/// Closed
		/// </summary>
		Closed = 5
	}

	/// <summary>
	/// Indicates source internal of the ticket raised
	/// </summary>
	public enum FreshdeskTicketSourceInternal : byte
	{
		/// <summary>
		/// 
		/// </summary>
		SEO = 1,
		/// <summary>
		/// 
		/// </summary>
		SEM = 2,
		/// <summary>
		/// 
		/// </summary>
		Email = 3,
		/// <summary>
		/// 
		/// </summary>
		Facebook = 4
	}

	/// <summary>
	/// Indicates source of the ticket raised
	/// </summary>
	public enum FreshdeskTicketSource
	{
		/// <summary>
		/// 
		/// </summary>
		Email = 1,
		/// <summary>
		/// 
		/// </summary>
		Portal = 2,
		/// <summary>
		/// 
		/// </summary>
		Phone = 3,
		/// <summary>
		/// 
		/// </summary>
		Forum = 4,
		/// <summary>
		/// 
		/// </summary>
		Twitter = 5,
		/// <summary>
		/// 
		/// </summary>
		Facebook = 6,
		/// <summary>
		/// 
		/// </summary>
		Chat = 7,
		/// <summary>
		/// 
		/// </summary>
		MobiHelp = 8,
		/// <summary>
		/// 
		/// </summary>
		FeedbackWidget = 9,
		/// <summary>
		/// 
		/// </summary>
		OutboundEmail = 10,
		/// <summary>
		/// 
		/// </summary>
		Ecommerce = 11
	}

	#endregion

	#region SMSProvider

	/// <summary>
	/// An enumeration for Enabling SMS service Provider
	/// </summary>
	public enum SMSProvider : byte
	{
		/// <summary>
		/// No SMSProvider is enabled
		/// </summary>
		None = 0,

		/// <summary>
		/// RouteSMS Provider
		/// </summary>
		RouteSMS = 1,

		/// <summary>
		/// ACL SMS Provider
		/// </summary>
		ACL = 2
	}

	#endregion
}