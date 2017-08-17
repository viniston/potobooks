using System;
using System.Web;
using Photo.Utility.LogHelper;

namespace Module
{
	/// <summary>
	/// Main HttpModule for CK.Presentation.Website
	/// </summary>
	public class PhotoHttpModule : IHttpModule
	{
		#region Private static members

		private static int _contextCount = 0;
		private static readonly object _firstInstanceSyncObject = new object();

		#endregion


		#region Private methods

		/// <summary>
		/// Perform startup actions and initialization tasks
		/// This method is called once per application pool
		/// </summary>
		private void InternalInit()
		{
			#region Log4net

			log4net.Config.XmlConfigurator.Configure();
			LogHelper.Log(Logger.Application, LogLevel.Info, "Application started");

			#endregion
		}

		#endregion


		#region IHttpModule interface

		public void Init(HttpApplication context)
		{
			#region Attach event handlers

			context.BeginRequest += new EventHandler(OnBeginRequest);
			context.EndRequest += new EventHandler(OnEndRequest);
			context.AuthenticateRequest += new EventHandler(OnAuthenticateRequest);
			context.PostAuthenticateRequest += new EventHandler(OnPostAuthenticateRequest);
			context.PostAuthorizeRequest += new EventHandler(OnPostAuthorizeRequest);
			context.Error += new EventHandler(OnError);

			#endregion

			//Initialize things
			if (_contextCount == 0)
			{
				lock(_firstInstanceSyncObject)
				{
					if (_contextCount == 0)
					{
						InternalInit();
					}
				}
			}

			_contextCount++;
		}

		public void Dispose()
		{
			_contextCount--;

			if(_contextCount == 0)
				LogHelper.Log(Logger.Application, LogLevel.Info, "Application ended");
		}

		#endregion


		#region Event handlers

		private void OnBeginRequest(object sender, EventArgs e)
		{
		}

		private void OnEndRequest(object sender, EventArgs e)
		{
		}

		private void OnError(object sender, EventArgs e)
		{
		}

		private void OnAuthenticateRequest(object sender, EventArgs e)
		{
		}

		private void OnPostAuthenticateRequest(object sender, EventArgs e)
		{
		}

		private void OnPostAuthorizeRequest(object sender, EventArgs e)
		{
		}

		#endregion
	}
}