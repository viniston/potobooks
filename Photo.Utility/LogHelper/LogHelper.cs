using log4net;
using System;
using System.Configuration;

namespace Photo.Utility.LogHelper
{
	/// <summary>
	/// The log Helper class
	/// </summary>
	public static class LogHelper
	{
		#region Private fields

		private static bool _enableDiagnosticLogging = false;

		#endregion


		#region Public properties

		/// <summary>
		/// This configuration value is used to enable/disable some diagnostic log messages
		/// The associated web.config key name is: EnableDiagnosticLogging
		/// </summary>
		public static bool EnableDiagnosticLogging
		{
			get
			{
				return _enableDiagnosticLogging;
			}
		}

		#endregion


		#region Static constructor

		static LogHelper()
		{
			#region EnableDiagnosticLogging

			if(ConfigurationManager.AppSettings["EnableDiagnosticLogging"] != null)
				if (!bool.TryParse(ConfigurationManager.AppSettings["EnableDiagnosticLogging"], out _enableDiagnosticLogging))
					_enableDiagnosticLogging = false;

			#endregion
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Logs the provided message to the appropriate logger
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="level"></param>
		/// <param name="message"></param>
		public static void Log(Logger logger, LogLevel level, string message)
		{
			Log(logger, level, message, null);
		}

		/// <summary>
		/// Logs the provided message to the appropriate logger
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="level"></param>
		/// <param name="exception"></param>
		public static void Log(Logger logger, LogLevel level, Exception exception)
		{
			Log(logger, level, string.Empty, exception);
		}

		/// <summary>
		/// Logs the provided message to the appropriate logger
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="level"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Log(Logger logger, LogLevel level, string message, Exception exception)
		{
			ILog iLogger = null;

			switch (logger)
			{
				case Logger.AuditTrail:
					iLogger = LogManager.GetLogger("AuditTrail");
					break;
				default:
					iLogger = LogManager.GetLogger("Application");
					break;
			}

			if (iLogger != null)
			{
				switch (level)
				{
					case LogLevel.Info:
						if (exception == null)
							iLogger.Info(message);
						else
							iLogger.Info(message, exception);
						break;

					case LogLevel.Warn:
						if (exception == null)
							iLogger.Warn(message);
						else
							iLogger.Warn(message, exception);

						break;

					default:
						if (exception == null)
							iLogger.Error(message);
						else
							iLogger.Error(message, exception);
						break;
				}
			}
		}

		#endregion
	}
}