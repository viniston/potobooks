using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Photo.Resources.Shared;
using Photo.Utility.LogHelper;

namespace Utility
{
	public static class Utilities
	{
		#region Public Methods

		/// <summary>
		/// GetErrorPage reads custom errors pages section from web.config and gets the default error page url
		/// </summary>
		/// <returns></returns>
		public static string GetErrorPage()
		{
			System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
			CustomErrorsSection section = (CustomErrorsSection)configuration.GetSection("system.web/customErrors");
			return section.DefaultRedirect.ToString();
		}

		/// <summary>
		/// Redirect to 404 page
		/// </summary>
		public static void RedirectToErrorPage()
		{
			System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
			CustomErrorsSection section = (CustomErrorsSection)configuration.GetSection("system.web/customErrors");
			HttpContext.Current.Response.Redirect(section.Errors["404"].Redirect);
		}

		/// <summary>
		/// Get Cross Page Message to be displayed on different page
		/// </summary>	
		public static CrossPageMessage GetCrossPageMessage(bool removeMessage)
		{
			if (HttpContext.Current.Session["CrossPageMessage"] == null || (!(HttpContext.Current.Session["CrossPageMessage"] is CrossPageMessage)))
			{
				return null;
			}

			CrossPageMessage crossPageMessage = (CrossPageMessage)HttpContext.Current.Session["CrossPageMessage"];

			if (removeMessage)
			{
				HttpContext.Current.Session.Remove("CrossPageMessage");
			}
			return crossPageMessage;
		}

		public static void ShowMessage(Literal ltlMessage, string messageText, MessageType messageType)
		{
			if (!string.IsNullOrEmpty(messageText) && messageText.Trim().Length > 0 && !ltlMessage.Text.Contains(messageText))
				ltlMessage.Text += Shared.Message.Replace("[MessageType]", messageType.ToString().ToLower()).Replace("[MessageText]", messageText);
		}

		/// <summary>
		/// Set Cross Page Message to be displayed on different page
		/// </summary>
		/// <param name="messageText"></param>
		/// <param name="messageType"></param>
		public static void SetCrossPageMessage(string messageText, MessageType messageType)
		{
			CrossPageMessage crossPageMessage = new CrossPageMessage(messageText, messageType);
			HttpContext.Current.Session["CrossPageMessage"] = crossPageMessage;
		}
		
		/// <summary>
		/// Create DataTabe from List
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		public static DataTable ConvertToDatatable<T>(List<T> data)
		{
			PropertyDescriptorCollection props =
				TypeDescriptor.GetProperties(typeof(T));
			DataTable table = new DataTable();
			for (int i = 0; i < props.Count; i++)
			{
				PropertyDescriptor prop = props[i];
				if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
					table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
				else
					table.Columns.Add(prop.Name, prop.PropertyType);
			}
			object[] values = new object[props.Count];
			foreach (T item in data)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = props[i].GetValue(item);
				}
				table.Rows.Add(values);
			}
			return table;
		}

		public static void ExportListing(string type, string formatName, string listName, object dataSource)
		{
			try
			{
				if (type == ".pdf")
				{
					HttpContext.Current.Response.ContentType = "application/pdf";
					HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + listName + ".pdf");
					HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
					StringWriter sw = new StringWriter();
					HtmlTextWriter hw = new HtmlTextWriter(sw);
					GridView GVExport = new GridView();
					GVExport.DataSource = dataSource;
					GVExport.DataBind();
					GVExport.RenderControl(hw);
					StringReader sr = new StringReader(sw.ToString());
					Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
					HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
					PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
					pdfDoc.Open();
					htmlparser.Parse(sr);
					pdfDoc.Close();
					HttpContext.Current.Response.Write(pdfDoc);
					HttpContext.Current.Response.Flush();
				}
				else {
					HttpContext.Current.Response.Clear();
					HttpContext.Current.Response.Charset = "";
					HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
					HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
					HttpContext.Current.Response.ContentType = "application/" + formatName + type;
					HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "Report" + type);

					StringWriter sw = new StringWriter();
					HtmlTextWriter htw = new HtmlTextWriter(sw);
					GridView GVExport = new GridView();
					GVExport.DataSource = dataSource;
					GVExport.DataBind();
					GVExport.RenderControl(htw);

					HttpContext.Current.Response.Write(sw);
					sw = null;
					htw = null;
					HttpContext.Current.Response.Flush();
					//HttpContext.Current.Response.End();
				}
			}
			catch(Exception ex)
			{
				LogHelper.Log(Logger.Application, LogLevel.Error, ex);				
			}
		}

		#endregion
	}

}