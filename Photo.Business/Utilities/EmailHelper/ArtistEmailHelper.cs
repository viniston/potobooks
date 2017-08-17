using System;
using System.Net;
using System.Net.Mail;
using Photo.Business.Entities.Configuration;
using Photo.Business.Entities.Security;
using Photo.Resources.Email;
using Photo.Business.Entities.Model;
using System.Collections.Generic;
using System.IO;
using Photo.Utility.LogHelper;

namespace Photo.Business.Utilities.EmailHelper
{
	public static class ArtistEmailHelper
	{
		public static bool SendDraftToCustomer(BookingInfo booking, string specialDescription)
        {
            try
            {
                string artistEmail = ArtistController.Instance[booking.ArtistID.Value].Email;
                string artistName = ArtistController.Instance[booking.ArtistID.Value].Name;

                List<Attachment> attachments = null;
                if (booking.ImageList.Count > 0)
                {
                    attachments = new List<Attachment>();
                    foreach (ImageInfo image in booking.ImageList)
                    {
                        Attachment attachment = new Attachment(image.Path);
                        attachment.ContentId = Path.GetFileName(image.Path);
                        attachments.Add(attachment);
                    }
                }

                return SendEmail(Email.SendOrderToArtistSubject.Replace("[SystemReference]", booking.SystemReference).Replace("[Product]", (booking.ProductCategory != null ? booking.ProductCategory.Name : "Custom image")),
                                Email.SendOrderToArtistBody.Replace("[Artist]", artistName).Replace("[SpecialInstruction]", "<strong>Comments</strong><br />" + specialDescription),
                                artistEmail,
                                string.Empty,
                                string.Empty,
                                attachments,
                                string.Empty);
            }
            catch(Exception ex)
            {
                LogHelper.Log(Logger.Application, LogLevel.Error, ex);
                return false;
            }

        }

        private static bool SendEmail(string subject, string body, string toAddress, string ccAddress, string bccAddress, List<Attachment> attachmentList, string messageDescription)
		{
			MailAddressCollection to = new MailAddressCollection();
			MailAddressCollection cc = new MailAddressCollection();
			MailAddressCollection bcc = new MailAddressCollection();

			if (!string.IsNullOrEmpty(toAddress))
				to.Add(toAddress);

			if (!string.IsNullOrEmpty(ccAddress))
				cc.Add(ccAddress);

			if (!string.IsNullOrEmpty(bccAddress))
				bcc.Add(bccAddress);

			return SendEmail(subject, body, to, cc, bcc, attachmentList, messageDescription);
		}

        private static bool SendEmail(string subject, string body, MailAddressCollection to, MailAddressCollection cc,
			MailAddressCollection bcc, List<Attachment> attachments, string messageDescription)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = true;
			mailMessage.Subject = subject;
			mailMessage.From = new MailAddress(ConfigurationController.Instance.ValueOf<string>("ArtistEmailSender.Username"), ConfigurationController.Instance.ValueOf<string>("ArtistEmailSender.DisplayName")); 

			mailMessage.ReplyToList.Add(mailMessage.From);

			foreach (MailAddress email in to)
			{
				if (cc != null && cc.Contains(email))
					cc.Remove(email);

				if (bcc != null && bcc.Contains(email))
					bcc.Remove(email);
			}

			mailMessage.To.Add(to.ToString());

			if (attachments != null)
			{
				foreach (var attachment in attachments)
				{
					mailMessage.Attachments.Add(attachment);
				}
			}

			foreach (MailAddress email in cc)
				if (bcc != null && bcc.Contains(email))
					bcc.Remove(email);

			SmtpClient smtpClient = new SmtpClient(ConfigurationController.Instance.ValueOf<string>("ArtistEmailSender.Smtp.Address"), ConfigurationController.Instance.ValueOf<int>("ArtistEmailSender.Smtp.Port"));
			smtpClient.EnableSsl = ConfigurationController.Instance.ValueOf<bool>("ArtistEmailSender.Smtp.EnableSSL");
			smtpClient.Credentials = new NetworkCredential(ConfigurationController.Instance.ValueOf<string>("ArtistEmailSender.Username"), ConfigurationController.Instance.ValueOf<string>("ArtistEmailSender.Password"));
			smtpClient.Timeout = ConfigurationController.Instance.ValueOf<int>("ArtistEmailSender.TimeoutMS");

			for (int i = 1; i <= ConfigurationController.Instance.ValueOf<int>("ArtistEmailSender.RetryCount"); i++)
			{
				try
				{
					smtpClient.Send(mailMessage);
					return true;
				}
				catch (Exception ex)
				{
					LogHelper.Log(Logger.Application, LogLevel.Error, ex);
					//Retry
				}
			}

			return false;
		}
	}
}