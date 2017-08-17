using System;
using System.Web;
using Task;

namespace AjaxProcessor
{
	public class AjaxRouter : IHttpHandler
	{
		public bool IsReusable
		{
			get { return false; }
		}

		public void ProcessRequest(HttpContext context)
		{
			string ajaxCmd = context.Request["cmd"];

			if(string.IsNullOrEmpty(ajaxCmd))
				throw new Exception("The command cannot be null or empty!");

			string responseData = null;

			switch (ajaxCmd)
			{
                case "getArtistFare":

                    break;	   
				default:
					throw new Exception("The command : " + ajaxCmd + " is invalid!");
			}

			if (responseData != null)
				HttpContext.Current.Response.Write(responseData);
		}
	}
}