
namespace Photo.Utility.LogHelper
{
	/// <summary>
	/// Enumeration for the types of different loggers used by the system
	/// </summary>
	public enum Logger : byte
	{
		Application,
		AuditTrail
	}

	/// <summary>
	/// Enumeration for the levels of logs
	/// </summary>
	public enum LogLevel : byte { Info, Warn, Error }
}