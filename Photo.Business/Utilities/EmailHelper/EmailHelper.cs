using System;
using System.Net;
using System.Net.Mail;
using Photo.Business.Entities.Configuration;
using Photo.Business.Entities.Security;
using Photo.Resources.Email;
using Photo.Business.Entities.Model;
using Photo.Utility.LogHelper;
using System.Collections.Generic;
using System.IO;
using Photo.Resources.PageLink;
using Photo.Business.Utilities.URL;

namespace Photo.Business.Utilities.EmailHelper
{
	public static class EmailHelper
	{
        public static bool WelcomeUserEmail(UserInfo user, string resetPasswordToken)
        {
            string emailBody = Email.WelcomeMail
                .Replace("[Name]", user.NameEN)
                .Replace("[EmailAddress]", user.UserName)
                .Replace("[Password]", resetPasswordToken);

            return SendEmail(Email.WelcomeSubject, emailBody, user.MembershipUser.Email, null, null, "Welcome");

        }

        public static bool NotifyPurchaseError(string queryString)
        {
            string emailBody = Email.NotifyPurchaseError
                .Replace("[Details]", queryString);
            return SendEmail(Email.NotifyPurchaseErrorSubject,
                        emailBody,
                        "support@cartoonkart.com",
                        null,
                        null,
                        "Thank you");
        }

		public static bool NotifyComplaint()
		{
			string emailBody = Email.NotifyComplaint;
			return SendEmail(Email.NotifyPurchaseErrorSubject,
						emailBody,
						"support@cartoonkart.com",
						null,
						null,
						"Thank you");
		}

		public static bool SendOrderAcknowledgement(BookingInfo booking)
        {
            string emailBody = Email.OrderAcknowledgement
                .Replace("[Dealer]", DealerController.Instance.GetByID(booking.DealerID.Value).Name)
                .Replace("[FirstName]", booking.FirstName)
                .Replace("[SystemReference]", booking.SystemReference);

            string emailAddresses = booking.Email;
            string acknowledgementEmailIds = ConfigurationController.Instance.ValueOf<string>("AcknowledmentEmailIDs");
            if (!string.IsNullOrEmpty(acknowledgementEmailIds))
                emailAddresses = booking.Email + "," + acknowledgementEmailIds;
            return SendEmail(Email.AddOrderConfirmationSubject
                                .Replace("[SystemReference]", booking.SystemReference),
                        emailBody.Replace("[Product]", booking.ProductCategoryID.HasValue ? booking.ProductCategory.Name : string.Empty),
                        emailAddresses,
                        null,
                        null,
                        "Thank you");
        }

        public static bool SendClearImageRequest(BookingInfo booking)
		{
			string emailBody = Email.SendClearImageRequestBody
                .Replace("[FirstName]", booking.FirstName)
				.Replace("[SystemReference]", booking.SystemReference);

            string emailAddresses = booking.Email;
            string acknowledgementEmailIds = ConfigurationController.Instance.ValueOf<string>("AcknowledmentEmailIDs");
            if(!string.IsNullOrEmpty(acknowledgementEmailIds))
                emailAddresses = booking.Email + "," + acknowledgementEmailIds;
            return SendEmail(Email.SendClearImageRequestSubject
                                .Replace("[SystemReference]", booking.SystemReference), 
                        emailBody.Replace("[Product]", booking.ProductCategoryID.HasValue ? "<p>Order Type: " + booking.ProductCategory.Name + "</p>" : string.Empty),
                        emailAddresses, 
                        null,
                        null,
                        "Thank you");
        }

        public static bool SendCustomerCopy(BookingInfo booking, ImageInfo image, bool isPreview)
        {
            try
            {
                List<Attachment> attachments = null;
                if (image != null)
                {
                    attachments = new List<Attachment>()
                    {
                        new Attachment(image.Path)
                        {
                            ContentId = Path.GetFileName(image.Path)
                        }
                    };
                }

                string previewUrl = URLHelper.GetServerURL(ServerURLType.HTTP) + PageLink.DownloadPreviewPage.Replace("[BookingID]", booking.ID.ToString()).Replace("[PurchaseID]", booking.PurchaseID.ToString());
                string subject = Email.SendImageToCustomerPreviewSubject.Replace("[SystemReference]", booking.SystemReference).Replace("[Name]", booking.FirstName + " " + booking.LastName);
                string body = isPreview ? Email.SendImageToCustomerPreviewBody
                                .Replace("[FirstName]", booking.FirstName)
                                .Replace("[PreviewUrl]", previewUrl)
                                .Replace("[DealerName]", booking.Dealer.Name)
                                .Replace("[DealerUrl]", booking.Dealer.Url) : 
                                Email.SendImageToCustomerDraftBody
                                .Replace("[FirstName]", booking.FirstName)
                                .Replace("[PreviewUrl]", previewUrl);
                string emailAddresses = booking.Email;
                string acknowledgementEmailIds = ConfigurationController.Instance.ValueOf<string>("AcknowledmentEmailIDs");
                if (!string.IsNullOrEmpty(acknowledgementEmailIds))
                    emailAddresses = booking.Email + "," + acknowledgementEmailIds;

                return SendEmail(subject, 
                                body,
                                emailAddresses,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                attachments);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Logger.Application, LogLevel.Error, ex);
                return false;
            }
        }

        public static string SendOrderText(BookingInfo booking)
        {
            return Email.OrderConfirmation
                .Replace("[Ref]", (booking != null ? booking.ID.ToString() : ""))
                .Replace("[Product]", (booking != null ? booking.ProductCategory.Name : "Not defined"))
                .Replace("[Amount]", "$ " + (booking != null ? booking.ProductCategory.Amount.ToString() : ""));
        }

        public static bool SendOrder(BookingInfo booking)
		{
			return SendEmail(Email.OrderConfirmationSubject, SendOrderText(booking), booking != null ? booking.Email : "support@cartoonkart.com", null, "support@cartoonkart.com", "Thank you!");
		}

        public static bool Contact(string description)
        {
            return SendEmail("Contact me", description, "support@cartoonkart.com", null, null, "Contact me!");
        }

        public static bool SendFailureNotice(BookingInfo booking)
        {
            string emailBody = Email.WelcomeMail
                .Replace("[Name]", booking.ProductCategory.Name)
                .Replace("[EmailAddress]", booking.Email)
                .Replace("[Password]", booking.PaymentList[0].PaymentStatus.ToString());

            return SendEmail(Email.WelcomeSubject, emailBody, booking.Email, null, "support@cartoonkart.com", "Welcome");

        }

        private static bool SendEmail(string subject, string body, string toAddress, string ccAddress, string bccAddress, string messageDescription, List<Attachment> attachmentList = null)
		{
			MailAddressCollection to = new MailAddressCollection();
			MailAddressCollection cc = new MailAddressCollection();
			MailAddressCollection bcc = new MailAddressCollection();

            foreach (string str in toAddress.Split(','))
            {
                if (!string.IsNullOrEmpty(str))
                    to.Add(str);
            }

			if (!string.IsNullOrEmpty(ccAddress))
				cc.Add(ccAddress);

			if (!string.IsNullOrEmpty(bccAddress))
				bcc.Add(bccAddress);

			return SendEmail(subject, body, to, cc, bcc, messageDescription, attachmentList);
		}

        private static bool SendEmail(string subject, string body, MailAddressCollection to, MailAddressCollection cc,
			MailAddressCollection bcc, string messageDescription, List<Attachment> attachmentList = null)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = true;
			mailMessage.Subject = subject;
			mailMessage.From = new MailAddress(ConfigurationController.Instance.ValueOf<string>("EmailSender.Username"), ConfigurationController.Instance.ValueOf<string>("EmailSender.DisplayName")); ;

			mailMessage.ReplyToList.Add(mailMessage.From);

			foreach (MailAddress email in to)
			{
				if (cc != null && cc.Contains(email))
					cc.Remove(email);

				if (bcc != null && bcc.Contains(email))
					bcc.Remove(email);
			}

			mailMessage.To.Add(to.ToString());

            if (attachmentList != null)
            {
                foreach (var attachment in attachmentList)
                    mailMessage.Attachments.Add(attachment);                
            }

            foreach (MailAddress email in cc)
				if (bcc != null && bcc.Contains(email))
					bcc.Remove(email);

			SmtpClient smtpClient = new SmtpClient(ConfigurationController.Instance.ValueOf<string>("EmailSender.Smtp.Address"), ConfigurationController.Instance.ValueOf<int>("EmailSender.Smtp.Port"));
			smtpClient.EnableSsl = ConfigurationController.Instance.ValueOf<bool>("EmailSender.Smtp.EnableSSL");
			smtpClient.Credentials = new NetworkCredential(ConfigurationController.Instance.ValueOf<string>("EmailSender.Username"), ConfigurationController.Instance.ValueOf<string>("EmailSender.Password"));
			smtpClient.Timeout = ConfigurationController.Instance.ValueOf<int>("EmailSender.TimeoutMS");

			for (int i = 1; i <= ConfigurationController.Instance.ValueOf<int>("EmailSender.RetryCount"); i++)
			{
				try
				{
					smtpClient.Send(mailMessage);
					return true;
				}
				catch (Exception ex)
				{
                    LogHelper.Log(Logger.Application, LogLevel.Error, 
                        "Exception while sending email (attempt "+ i.ToString() +") to " + to.ToString() + ". Exception details : \n" + ex.Message);
					//Retry
				}
			}

			return false;
		}


	}
}