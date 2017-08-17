using System;

namespace Utility
{
	[Serializable]
	public class CrossPageMessage
	{
		#region Private Members

		private string _messageText;
		private MessageType _messageType;

		#endregion


		#region Public Members/Properties

		public string MessageText
		{
			get
			{
				return _messageText;
			}
			set
			{
				_messageText = value;
			}
		}

		public MessageType MessageType
		{
			get
			{
				return _messageType;
			}
			set
			{
				_messageType = value;
			}
		}

		#endregion


		#region Constructors

		public CrossPageMessage(string messageText, MessageType messageType)
		{
			_messageText = messageText;
			_messageType = messageType;
		}

		#endregion
	}
}
