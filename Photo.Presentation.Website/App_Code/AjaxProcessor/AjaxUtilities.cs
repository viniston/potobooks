using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Photo.Utility.LogHelper;

namespace AjaxProcessor
{
	/// <summary>
	/// Helper utilities to help us out with our Ajax Handlers
	/// </summary>
	public static class AjaxUtilities
	{
		#region Internal Functions

		internal static T GetRequestObject<T>(HttpRequest request) where T : class
		{
			string jsonString;
			request.InputStream.Position = 0;

			using (StreamReader inputStream = new StreamReader(request.InputStream))
			{
				jsonString = inputStream.ReadToEnd();
			}

			T requestObject = null;

			try
			{
				requestObject = JsonConvert.DeserializeObject<T>(jsonString);
			}
			catch(Exception ex)
			{
				LogHelper.Log(Logger.Application, LogLevel.Error, String.Format("Invalid Ajax request sent. Posted data: {0}", jsonString), ex);
			}

			return requestObject;
		}
		
		#endregion
	}
}