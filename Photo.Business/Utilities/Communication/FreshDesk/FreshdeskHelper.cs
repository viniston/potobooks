using System;
using System.IO;
using System.Net;
using System.Text;
using Photo.Business.Entities.Configuration;
using Photo.Business.Entities.Model;
using Photo.Utility.LogHelper;
using Newtonsoft.Json.Linq;

namespace Photo.Business.Utilities.Communication
{
    /// <summary>
    /// Freshdesk Helper class
    /// </summary>
    public static class FreshdeskHelper
	{
		#region Private Methods

		private static string GetFreshdeskAPIKey()
		{
            return ConfigurationController.Instance.ValueOf<string>("FreshdeskApiKey");
		}

		private static string GetFreshdeskCreateTicketUrl()
		{
            return ConfigurationController.Instance.ValueOf<string>("FreshdeskCreateTicketUrl");            
		}

		private static string GetFreshdeskAccountID()
		{
            return ConfigurationController.Instance.ValueOf<string>("FreshdeskAccountID");            
		}

		private static bool GetFreshdeskEnableStatus()
		{
            return ConfigurationController.Instance.ValueOf<bool>("FreshdeskEnable");            
		}

		private static string GetFreshdeskCKHelpEmail()
		{
            return ConfigurationController.Instance.ValueOf<string>("FreshdeskCKHelp");			
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// This method raise the request to freshdesk to create a ticket based on market profile id.
		/// </summary>
		/// <param name="freshdeskTicket"></param>
		/// <param name=""></param>
		public static void CreateTicket(FreshdeskTicket freshdeskTicket)
		{
			try
			{
				if (!GetFreshdeskEnableStatus())
					return;

				JObject customFields = new JObject
				{
					{"pax_name_" + GetFreshdeskAccountID(), freshdeskTicket.Name},
					{"customer_contact_no_" + GetFreshdeskAccountID(), freshdeskTicket.ContactNumber},
					{"customer_mail_id_" + GetFreshdeskAccountID(), freshdeskTicket.Email},
					{"source_internal_" + GetFreshdeskAccountID(), freshdeskTicket.SourceInternal.ToString()},
					{"ck_booking_reference_" + GetFreshdeskAccountID(), freshdeskTicket.CKReferenceNumber},
					{"url_" + GetFreshdeskAccountID(), freshdeskTicket.URL},
					{"trip_type_" + GetFreshdeskAccountID(), freshdeskTicket.TripType}
				};

				JObject helpdeskTicket = new JObject
				{
					{"description_html", freshdeskTicket.DescriptionHTML.Replace("\"", "'").Replace("\r\n", "").Replace("\t", "")},
					{"subject", freshdeskTicket.Subject},
					{"name", freshdeskTicket.Name},
					{"email", freshdeskTicket.Email},
					{"phone", freshdeskTicket.ContactNumber},
					{"priority", (int) freshdeskTicket.Priority},
					{"status", (int) freshdeskTicket.Staus}
				};

				helpdeskTicket.Add("ticket_type", "caricature");
				
				// If the source is not set, the value is hardcoded to Email 
				helpdeskTicket.Add("source", (freshdeskTicket.Source.HasValue ? (int)freshdeskTicket.Source.Value : (int)FreshdeskTicketSource.Email));

				helpdeskTicket.Add("to_email", GetFreshdeskCKHelpEmail());
				helpdeskTicket.Add("custom_field", customFields);

				JArray CCEmailArray = new JArray();
				if (freshdeskTicket.CcEmails != null)
					CCEmailArray.Add(freshdeskTicket.CcEmails.ToArray());

				JObject helpdesk_ticket = new JObject { { "helpdesk_ticket", helpdeskTicket }, { "cc_emails", CCEmailArray } };

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetFreshdeskCreateTicketUrl());

				request.ContentType = "application/json";
				request.Method = "POST";

				string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(GetFreshdeskAPIKey()));
				request.Headers["Authorization"] = "Basic " + authInfo;

                LogHelper.Log(Logger.Application, LogLevel.Info, "Triggering Helpdesk");
				StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
				requestWriter.Write(helpdesk_ticket.ToString());
                LogHelper.Log(Logger.Application, LogLevel.Info, helpdesk_ticket.ToString());
                requestWriter.Close();

				using (WebResponse response = request.GetResponse())
				{
					StreamReader responseReader = new StreamReader(response.GetResponseStream());
					responseReader.ReadToEnd();
					responseReader.Close();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log(Logger.Application, LogLevel.Error, "Freshdesk create ticket failed due to: " + ex.Message);
			}
		}

		/// <summary>
		/// Method to create freshdesk ticket.
		/// </summary>
		/// <param name="bookingFile"></param>
		/// <param name="url"></param>
		public static void CreateFreshdeskTicketForTicketFailure(BookingInfo bookingFile, string url)
		{

		}
        
		#endregion
	}
}
