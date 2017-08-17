using Photo.Business.Entities.Common;
using System;

namespace Photo.Business.Entities.Model.Common.Currency
{
	/// <summary>
	/// The Info class for Currency
	/// </summary>
	public class CurrencyInfo : ICBO<short>
	{
		#region Private Members

		private short _id;
		private string _nameSingularEN;
		private string _namePluralEN;
		private string _symbol;
		private string _isoCode3;
		private string _isoCodeNumeric;
		private byte _decimalDigits;		
		

		#endregion


		#region Public Properties

		public short ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public string NameSingularEN
		{
			get { return _nameSingularEN; }
			set { _nameSingularEN = value; }
		}

		public string NamePluralEN
		{
			get { return _namePluralEN; }
			set { _namePluralEN = value; }
		}

		public string Symbol
		{
			get { return _symbol; }
			set { _symbol = value; }
		}

		public string ISOCode3
		{
			get { return _isoCode3; }
			set { _isoCode3 = value; }
		}

		public string ISOCodeNumeric
		{
			get { return _isoCodeNumeric; }
			set { _isoCodeNumeric = value; }
		}

		public byte DecimalDigits
		{
			get { return _decimalDigits; }
			set { _decimalDigits = value; }
		}

		public bool? IsActive { get; set; }

		public DateTime Updated { get; set; }

		public short Identity => ID;

		#endregion
	}
}