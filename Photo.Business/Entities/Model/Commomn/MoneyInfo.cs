using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Photo.Business.Entities.Model.Common.Currency;
using Photo.Business.Entities.Model;

namespace Photo.Business.Entities.Model.Common.Money
{
	/// <summary>
	/// The Info class for Money
	/// </summary>
	[Serializable]
	public class MoneyInfo : IComparable
	{
		#region Private Members

		private short? _currencyID;
		private decimal _value = 0;

		#endregion

		
		#region Public Properties

		[XmlElement("CID")]
		public short? CurrencyID
		{
			get { return _currencyID; }
			set { _currencyID = value; }
		}

		[XmlIgnore]
		public CurrencyInfo Currency
		{
			get { if (CurrencyID.HasValue) return CurrencyController.Instance.GetByID(CurrencyID.Value); else return null; }

			set
			{
				if (value == null)
					_currencyID = null;
				else
					_currencyID = value.ID;
			}
		}

		[XmlElement("Value")]
		public decimal Value
		{
			get { return _value; }
			set { _value = value; }
		}

		[XmlIgnore]
		public string FormatString
		{
			get { return "0" + (Currency.DecimalDigits > 0 ? ".".PadRight(Currency.DecimalDigits + 1, '0') : string.Empty); }
		}

		[XmlIgnore]
		public string FormattedValue
		{
			get { return Value.ToString(FormatString); }
		}
		
		#endregion

		
		#region Public Constructors

		/// <summary>
		/// Creates a new MoneyInfo object
		/// </summary>
		public MoneyInfo() { }

		/// <summary>
		/// Creates a new MoneyInfo by parsing the provided string value
		/// </summary>
		/// <param name="str"></param>
		public MoneyInfo(string str)
		{
			str = str.Trim().Replace(" ", "");

			if (str.Length >= 3)
				this.Currency = CurrencyController.Instance.GetByISOCode3(str.Substring(0, 3));

			if (str.Length > 3)
				this.Value = Convert.ToDecimal(str.Substring(3, str.Length - 3));
		}

		/// <summary>
		/// Creates a new MoneyInfo object that corresponds to the provided value and currency
		/// </summary>
		/// <param name="currency"></param>
		public MoneyInfo(CurrencyInfo currency)
		{
			this.Currency = currency;
		}

		/// <summary>
		/// Creates a new MoneyInfo object that corresponds to the provided value and currency
		/// </summary>
		/// <param name="value"></param>
		/// <param name="currency"></param>
		public MoneyInfo(decimal value, CurrencyInfo currency)
		{
			this.Value = value;
			this.Currency = currency;
		}

		/// <summary>
		/// Creates a new MoneyInfo object that corresponds to the provided value and currency
		/// </summary>
		/// <param name="currencyID"></param>
		public MoneyInfo(short currencyID)
		{
			this.CurrencyID = currencyID;
		}

		/// <summary>
		/// Creates a new MoneyInfo object that corresponds to the provided value and currency
		/// </summary>
		/// <param name="value"></param>
		/// <param name="currencyID"></param>
		public MoneyInfo(decimal value, short currencyID)
		{
			this.Value = value;
			this.CurrencyID = currencyID;
		}

		/// <summary>
		/// Creates a new MoneyInfo object that corresponds to the provided value and currency
		/// </summary>
		/// <param name="value"></param>
		/// <param name="currencyISOCode3"></param>
		public MoneyInfo(decimal value, string currencyISOCode3)
		{
			this.Value = value;
			this.CurrencyID = CurrencyController.Instance.GetByISOCode3(currencyISOCode3).ID;
		}

		#endregion


		#region Public overrides and operator overloads

		/// <summary>
		/// Returns a formatted value based on the currency's decimal digits' with the currency ISO Code 3
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			if (CurrencyID.HasValue)
				return Currency.ISOCode3 + " " + FormattedValue;
			else
				return null;
		}

		/// <summary>
		/// Equals check
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>bool</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (obj is MoneyInfo)
			{
				MoneyInfo otherObject = (MoneyInfo)obj;
				
				if (this._currencyID.HasValue && otherObject._currencyID.HasValue)
					return (this._currencyID.Value == otherObject._currencyID.Value && this.Value == otherObject.Value);
				else if (!this._currencyID.HasValue && !otherObject._currencyID.HasValue)
					return true;
				else
					return false;
			}
			else
			{
				throw new ArgumentException("object is not a MoneyInfo");
			}
		}

		/// <summary>
		/// Equality check
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>bool</returns>
		public static bool operator ==(MoneyInfo m1, MoneyInfo m2)
		{
			bool m1null, m2null;
			m1null = Object.ReferenceEquals(m1, null);
			m2null = Object.ReferenceEquals(m2, null);
			if (m1null && m2null) return true;
			if (m1null || m2null) return false;
			return m1.Equals(m2);
		}

		/// <summary>
		/// Non-Equality check
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>bool</returns>
		public static bool operator !=(MoneyInfo m1, MoneyInfo m2)
		{
			bool m1null, m2null;
			m1null = Object.ReferenceEquals(m1, null);
			m2null = Object.ReferenceEquals(m2, null);
			if (m1null && m2null) return false;
			if (m1null || m2null) return true;
			return !m1.Equals(m2);
		}

		/// <summary>
		/// Addition operation
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo operator +(MoneyInfo m1, MoneyInfo m2)
		{
			if (m1 == null || m2 == null)
			{
				if (m1 != null)
					return new MoneyInfo(m1.Value, m1.Currency);
				else if (m2 != null)
					return new MoneyInfo(m2.Value, m2.Currency);
				else
					return null;
			}
			else
			{
				if ((m1.CurrencyID == m2.CurrencyID) ||
					(m1.CurrencyID.HasValue && !m2.CurrencyID.HasValue) ||
					(!m1.CurrencyID.HasValue && m2.CurrencyID.HasValue))
				{
					if (!m1.CurrencyID.HasValue)
						m1.CurrencyID = m2.CurrencyID;
					else if (!m2.CurrencyID.HasValue)
						m2.CurrencyID = m1.CurrencyID;

					MoneyInfo money = new MoneyInfo();
					money.CurrencyID = m1.CurrencyID;
					money.Value = m1.Value + m2.Value;
					return money;
				}
				else
				{
					throw new Exception("Must have simillar Currencies to calculate correctly");
				}
			}
		}

		/// <summary>
		/// Subtraction operation
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo operator -(MoneyInfo m1, MoneyInfo m2)
		{
			if (m1 == null || m2 == null)
			{
				if (m1 != null)
					return new MoneyInfo(m1.Value, m1.Currency);
				else if (m2 != null)
					return new MoneyInfo(-m2.Value, m2.Currency);
				else
					return null;
			}
			else
			{
				if ((m1.CurrencyID == m2.CurrencyID) ||
					(m1.CurrencyID.HasValue && !m2.CurrencyID.HasValue) ||
					(!m1.CurrencyID.HasValue && m2.CurrencyID.HasValue))
				{
					if (!m1.CurrencyID.HasValue)
						m1.CurrencyID = m2.CurrencyID;
					else if (!m2.CurrencyID.HasValue)
						m2.CurrencyID = m1.CurrencyID;

					MoneyInfo money = new MoneyInfo();
					money.CurrencyID = m1.CurrencyID;
					money.Value = m1.Value - m2.Value;
					return money;
				}
				else
				{
					throw new Exception("Must have simillar Currencies to calculate correctly");
				}
			}
		}

		/// <summary>
		/// Multiplication operation
		/// </summary>
		/// <param name="d"></param>
		/// <param name="m"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo operator *(object d, MoneyInfo m)
		{
			return (m * Convert.ToDecimal(d));
		}

		/// <summary>
		/// Multiplication operation
		/// </summary>
		/// <param name="m"></param>
		/// <param name="d"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo operator *(MoneyInfo m, object d)
		{
			if (m.CurrencyID.HasValue)
				return new MoneyInfo(m.Value * Convert.ToDecimal(d), m.Currency);
			else
				throw new Exception("Must have a Currency to calculate correctly");
		}

		/// <summary>
		/// Division operation
		/// </summary>
		/// <param name="m"></param>
		/// <param name="d"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo operator /(MoneyInfo m, object d)
		{
			if (m.CurrencyID.HasValue)
				return new MoneyInfo(m.Value / Convert.ToDecimal(d), m.Currency);
			else
				throw new Exception("Must have a Currency to calculate correctly");
		}

		/// <summary>
		/// Greater-than check
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>bool</returns>
		public static bool operator >(MoneyInfo m1, MoneyInfo m2)
		{
			if (m1.CurrencyID == m2.CurrencyID)
				return (m1.Value > m2.Value);
			else
				throw new Exception("Must have simillar Currencies to calculate correctly");
		}

		/// <summary>
		/// Less-than check
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>bool</returns>
		public static bool operator <(MoneyInfo m1, MoneyInfo m2)
		{
			if (m1.CurrencyID == m2.CurrencyID)
				return (m1.Value < m2.Value);
			else
				throw new Exception("Must have simillar Currencies to calculate correctly");
		}

		/// <summary>
		/// Greater-than-or-equal check
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>bool</returns>
		public static bool operator >=(MoneyInfo m1, MoneyInfo m2)
		{
			if (m1.CurrencyID == m2.CurrencyID)
				return (m1.Value >= m2.Value);
			else
				throw new Exception("Must have simillar Currencies to calculate correctly");
		}

		/// <summary>
		/// Less-than-or-equal check
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>bool</returns>
		public static bool operator <=(MoneyInfo m1, MoneyInfo m2)
		{
			if (m1.CurrencyID == m2.CurrencyID)
				return (m1.Value <= m2.Value);
			else
				throw new Exception("Must have simillar Currencies to calculate correctly");
		}

		#endregion


		#region IComparable Members

		/// <summary>
		/// Comparison operation
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>int</returns>
		int IComparable.CompareTo(object obj)
		{
			return this.CompareTo(obj);
		}

		/// <summary>
		/// Comparison operation
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>int</returns>
		public int CompareTo(object obj)
		{
			if (obj is MoneyInfo)
			{
				MoneyInfo otherObject = (MoneyInfo)obj;

				if (this.CurrencyID == otherObject.CurrencyID)
					return this.Value.CompareTo(otherObject.Value);
				else
					throw new Exception("Must have simillar Currencies to calculate correctly");
			}
			else
			{
				throw new ArgumentException("object is not a MoneyInfo");
			}			
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Sets current value to the largest integer less than or equal to it
		/// </summary>
		public void FloorValue()
		{
			Value = Math.Floor(Value);
		}

		/// <summary>
		/// Sets current value to the smallest integer greater than or equal to it
		/// </summary>
		public void CeilValue()
		{
			Value = Math.Ceiling(Value);
		}

		/// <summary>
		/// Rounds current value to the currency's decimal digits count
		/// </summary>
		public void RoundValue()
		{
			if (CurrencyID.HasValue)
				Value = Math.Round(Value, Currency.DecimalDigits);
		}

		/// <summary>
		/// Rounds current value to the provided decimal digits count
		/// </summary>
		public void RoundValue(int decimals)
		{
			Value = Math.Round(Value, decimals);
		}

		/// <summary>
		/// Sets current value to its absolute (magnitude) value
		/// </summary>
		public void MagnitudeValue()
		{
			Value = Math.Abs(Value);
		}

		/// <summary>
		/// Implementation for the override of the ICloneable's Clone method
		/// </summary>
		/// <returns>MoneyInfo</returns>
		public MoneyInfo Clone()
		{
			return new MoneyInfo(Value, Currency);
		}

		#endregion
	}
}